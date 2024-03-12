using System.Data;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.core;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.EAV;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.Dys;
using amorphie.contract.core.Services;
using amorphie.contract.core.Services.Kafka;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.infrastructure.Extensions;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;


namespace amorphie.contract.application
{

    public class DocumentAppService : IDocumentAppService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IMinioService _minioService;
        private readonly ITemplateEngineService _templateEngineService;

        private readonly IDysProducer _dysProducer;

        public DocumentAppService(ProjectDbContext projectDbContext, IMinioService minioService, IDysProducer dysProducer)
        {
            _dbContext = projectDbContext;
            _minioService = minioService;
            _dysProducer = dysProducer;
        }

        public async Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken)
        {

            var response = new List<RootDocumentDto>();

            var query = _dbContext!.Document
                                    .Skip(input.Page * input.PageSize)
                                    .Take(input.PageSize);


            if (!string.IsNullOrEmpty(input.Keyword))
            {
                query = query.Where(x => x.Status.ToString() == input.Keyword);
            }

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {

                response = mapToRootDocumentDto(securityQuestions);
            }

            return GenericResult<List<RootDocumentDto>>.Success(response);
        }


        public async Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentAll(CancellationToken cancellationToken)
        {
            var query = _dbContext!.Document;
            var response = new List<RootDocumentDto>();

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {
                response = mapToRootDocumentDto(securityQuestions);
            }

            return GenericResult<List<RootDocumentDto>>.Success(response);
        }

        private List<DocumentElementDto> GetDocumentElementDtos(string Fields, string TitleFields)
                {
                    var elementIds = Fields.Split(',').Select(x => x.Trim());
                    var elementTitles = TitleFields.Split(',').Select(x => x.Trim());

                    var documentElementDtos = elementIds.Zip(elementTitles, (id, title) =>
                        new DocumentElementDto
                        {
                            ElementID = id,
                            ElementName = title
                        }).ToList();

                    return documentElementDtos;
                }

        public async Task<GenericResult<bool>> Instance(DocumentInstanceInputDto input)
        {

            var docdef = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == input.DocumentCode && x.Semver == input.DocumentVersion);
            if (docdef == null)
            {
                throw new ArgumentException("Document Code ve versiyona ait kayit bulunamadi!");
            }

            var entityProperties = ObjectMapperApp.Mapper.Map<List<EntityProperty>>(input.EntityPropertyDtos);
            if (docdef.DocumentEntityPropertys.Any() && docdef.DocumentDys!=null)
            {
                var element = GetDocumentElementDtos(docdef.DocumentDys.Fields,docdef.DocumentDys.TitleFields);
                foreach (var entityProperty in entityProperties)
                {
                    var correspondingElement = element.FirstOrDefault(e => e.ElementName == entityProperty.Code);
                    if (correspondingElement != null)
                    {
                        entityProperty.Code = correspondingElement.ElementID;
                    }
                }
                foreach (var item in docdef.DocumentEntityPropertys)
                {
                    if (item.EntityProperty.Required && string.IsNullOrEmpty(item.EntityProperty.EntityPropertyValue.Data))
                    {
                        var conflictingProperty = entityProperties.FirstOrDefault(x => x.Code == item.EntityProperty.Code);
                        if (conflictingProperty != null)
                        {
                            throw new ArgumentException($"Girilmesi zorunlu metadalar bulunmakta {item.EntityProperty.Code}");
                        }
                    }
                }
            }
    

            var cus = _dbContext.Customer.FirstOrDefault(x => x.Reference == input.Reference);
            if (cus == null)
            {
                cus = new core.Entity.Customer
                {
                    Reference = input.Reference,
                    Owner = input.Owner,
                    CustomerNo = input.CustomerNo
                };

                _dbContext.Customer.Add(cus);
                _dbContext.SaveChanges();

            }

            var document = new Document
            {
                Id = input.Id,
                DocumentDefinitionId = docdef.Id,
                Status = EStatus.Completed,
                CustomerId = cus.Id,
                DocumentContent = ObjectMapperApp.Mapper.Map<DocumentContent>(input),
                DocumentInstanceNotes = ObjectMapperApp.Mapper.Map<List<DocumentInstanceNote>>(input.NoteDtos),
            }; //DocumentInstanceNotes dan hata alcak mıyım kontrol et

            if (entityProperties.Any())
            {
                document.DocumentInstanceEntityPropertys = entityProperties
                    .Select(item => new DocumentInstanceEntityProperty
                    {
                        DocumentId = document.Id,
                        EntityProperty = item
                    })
                    .ToList();
            }

            _dbContext.Document.Add(document);
            await _dbContext.SaveChangesAsync();

            byte[] fileByteArray;
            if (input.FileContextType == "byte")
            {
                fileByteArray = input.FileContext.Split(',').Select(byte.Parse).ToArray();
            }
            else if (input.FileContextType == "ZeebeRender")
            {
                fileByteArray = Convert.FromBase64String(input.FileContext);//TODO: SubFlow için düzenle
            }
            else if (input.FileContextType == "TemplateRender")
            {
                // var content = _templateEngineService.GetRenderInstance(input.FileContext).Result.ToString().Trim('\"');
                var content = await GetRenderInstance(input.FileContext);
                content = content.Trim('\"');
                fileByteArray = Convert.FromBase64String(content);//TODO: SubFlow için düzenle
            }
            else
            {
                fileByteArray = Convert.FromBase64String(input.FileContext);
            }

            await _minioService.UploadFile(fileByteArray, input.ToString(), input.FileType, "");

            if (docdef.DocumentDys is not null)
            {
                await SendToDys(docdef, input, fileByteArray);
            }

            return GenericResult<bool>.Success(true);
        }

        private async Task SendToDys(DocumentDefinition docDef, DocumentInstanceInputDto inputDto, byte[] fileByteArray)
        {
            var documentDys = new DocumentDysRequestModel(inputDto.Owner, inputDto.DocumentCode, docDef.DocumentDys.ReferenceId.ToString(), docDef.Code, inputDto.ToString(), inputDto.FileType, fileByteArray);
            if (inputDto.EntityPropertyDtos is not null)
            {
                foreach (var item in inputDto.EntityPropertyDtos)
                {
                    documentDys.DocumentParameters.Add(item.Code, item.EntityPropertyValue);
                }
            }
            await _dysProducer.PublishDysData(documentDys);
        }

        public async Task<string> GetRenderInstance(string instance)//TODO:dapr kullanılacak 
        {
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(StaticValuesExtensions.TemplateEngineUrl + string.Format(StaticValuesExtensions.TemplateEngineRenderInstance, instance));

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

            }
            return "Template engine error";
        }

        public async Task<GenericResult<List<RootDocumentDto>>> GetAllMethod(PagedInputDto pagedInputDto, CancellationToken cancellationToken)
        {
            var query = _dbContext!.Document;
            var response = new List<RootDocumentDto>();

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {
                response = mapToRootDocumentDto(securityQuestions);
            }

            return GenericResult<List<RootDocumentDto>>.Success(response);
        }

        private List<RootDocumentDto> mapToRootDocumentDto(List<Document> documents)
        {
            return documents.Select(x =>
                         new RootDocumentDto
                         {
                             Id = x.Id,
                             DocumentDefinitionId = x.DocumentDefinitionId.ToString(),
                             StatuCode = x.Status.ToString(),
                             CreatedAt = x.CreatedAt,
                             DocumentDefinition = new DocumentDefinitionDto
                             {
                                 Code = x.DocumentDefinition.Code,
                                 MultilanguageText = ObjectMapperApp.Mapper.Map<List<MultilanguageText>>(x.DocumentDefinition.DocumentDefinitionLanguageDetails),
                                 DocumentOperations = new DocumentOperationsDto
                                 {
                                     DocumentManuelControl = x.DocumentDefinition.DocumentOperations!.DocumentManuelControl,
                                     DocumentOperationsTagsDetail = ObjectMapperApp.Mapper.Map<List<TagDto>>(x.DocumentDefinition.DocumentOperations.DocumentOperationsTagsDetail!.Select(t => t.Tags))
                                 }
                             },
                             DocumentContent = ObjectMapperApp.Mapper.Map<DocumentContentDto>(x.DocumentContent)
                         }).ToList();

        }

        public async Task<GenericResult<ReleaseableFileStreamModel>> DownloadDocument(DocumentDownloadInputDto inputDto, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(inputDto.ObjectName, out Guid contentId))
            {
                throw new FormatException("ObjectName is not in a valid Guid format.");
            }

            var userReference = inputDto.GetUserReference();

            var customerDoc = await _dbContext.Document.FirstOrDefaultAsync(
               c => c.Customer != null && c.Customer.Reference == userReference && c.DocumentContentId == contentId);

            if (customerDoc is null)
                throw new FileNotFoundException($"{inputDto.ObjectName} file not found for {userReference}");

            var res = await _minioService.DownloadFile(customerDoc.DocumentContent.MinioObjectName, cancellationToken);
            return GenericResult<ReleaseableFileStreamModel>.Success(res);
        }
    }

    public interface IDocumentAppService
    {
        public Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken);
        public Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentAll(CancellationToken cancellationToken);
        Task<GenericResult<bool>> Instance(DocumentInstanceInputDto input);
        Task<GenericResult<ReleaseableFileStreamModel>> DownloadDocument(DocumentDownloadInputDto inputDto, CancellationToken cancellationToken);
    }
}