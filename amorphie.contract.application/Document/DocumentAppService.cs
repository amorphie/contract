using System.Data;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.core;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.EAV;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.Colleteral;
using amorphie.contract.core.Model.Dys;
using amorphie.contract.core.Response;
using amorphie.contract.core.Services;
using amorphie.contract.core.Services.Kafka;
using amorphie.contract.infrastructure.Contexts;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace amorphie.contract.application
{

    public class DocumentAppService : IDocumentAppService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IMinioService _minioService;

        private readonly IDysProducer _dysProducer;

        private readonly ITSIZLProducer _tsizlProducer;


        public DocumentAppService(ProjectDbContext projectDbContext, IMinioService minioService, IDysProducer dysProducer, ITSIZLProducer tsizlProducer)
        {
            _dbContext = projectDbContext;
            _minioService = minioService;
            _dysProducer = dysProducer;
            _tsizlProducer = tsizlProducer;
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

            var entityProperties = ObjectMapperApp.Mapper.Map<List<EntityProperty>>(input.InstanceMetadata);
            if (docdef.DocumentEntityPropertys.Any() && docdef.DocumentDys != null)
            {
                var element = GetDocumentElementDtos(docdef.DocumentDys.Fields, docdef.DocumentDys.TitleFields);
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
                        if (conflictingProperty == null)
                        {
                            throw new ArgumentException($"Girilmesi zorunlu metadalar bulunmakta {item.EntityProperty.Code}");
                        }
                    }
                    //Else if(item.EntityProperty.Required) -> tagdan gelecek...
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
                DocumentInstanceNotes = ObjectMapperApp.Mapper.Map<List<DocumentInstanceNote>>(input.Notes),
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

            if (docdef.DocumentTsizl is not null)
            {
                var request = new DoAutomaticEngagementPlainRequestDto(Convert.ToInt32(input.CustomerNo), docdef.DocumentTsizl.EngagementKind, StaticValuesExtensions.Fora.UserCode);
                await _tsizlProducer.PublishTSIZLData(request);
            }

            return GenericResult<bool>.Success(true);
        }

        private async Task SendToDys(DocumentDefinition docDef, DocumentInstanceInputDto inputDto, byte[] fileByteArray)
        {
            var documentDys = new DocumentDysRequestModel(inputDto.Owner, inputDto.DocumentCode, docDef.DocumentDys.ReferenceId.ToString(), docDef.Code, inputDto.ToString(), inputDto.FileType, fileByteArray);
            if (inputDto.InstanceMetadata is not null)
            {
                foreach (var item in inputDto.InstanceMetadata)
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

        public async Task<GenericResult<DocumentDownloadOutputDto>> DownloadDocument(DocumentDownloadInputDto inputDto, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(inputDto.ObjectId, out Guid contentId))
            {
                return GenericResult<DocumentDownloadOutputDto>.Fail("ObjectId is not in a valid Guid format");
            }

            var userReference = inputDto.GetUserReference();

            var customerDoc = await _dbContext.Document.AsNoTracking().FirstOrDefaultAsync(
               c => c.Customer != null && c.Customer.Reference == userReference && c.DocumentContentId == contentId);

            if (customerDoc is null)
                throw new FileNotFoundException($"{inputDto.ObjectId} file not found for {userReference}");

            var minioObject = await _minioService.DownloadFile(customerDoc.DocumentContent.MinioObjectName, cancellationToken);

            var res = new DocumentDownloadOutputDto
            {
                FileType = minioObject.ContentType,
                FileContent = minioObject.FileContent,
                FileName = minioObject.FileName
            };

            return GenericResult<DocumentDownloadOutputDto>.Success(res);
        }
    }

    public interface IDocumentAppService
    {
        public Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken);
        public Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentAll(CancellationToken cancellationToken);
        Task<GenericResult<bool>> Instance(DocumentInstanceInputDto input);
        Task<GenericResult<DocumentDownloadOutputDto>> DownloadDocument(DocumentDownloadInputDto inputDto, CancellationToken cancellationToken);
    }
}