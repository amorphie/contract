using System.Data;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.core;
using amorphie.contract.core.Entity;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.EAV;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.Dys;
using amorphie.contract.core.Services;
using amorphie.contract.core.Services.Kafka;
using amorphie.contract.infrastructure.Contexts;
using amorphie.core.Base;
using amorphie.core.Enums;
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

        public async Task<List<RootDocumentDto>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken)
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

            return response;
        }


        public async Task<List<RootDocumentDto>> GetAllDocumentAll(CancellationToken cancellationToken)
        {
            var query = _dbContext!.Document;
            var response = new List<RootDocumentDto>();

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {
                response = mapToRootDocumentDto(securityQuestions);
            }

            return response;
        }

        public async Task<bool> Instance(DocumentInstanceInputDto input)
        {

            var docdef = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == input.DocumentCode && x.Semver == input.DocumentVersion);
            if (docdef == null)
            {
                throw new ArgumentException("Document Code ve versiyona ait kayit bulunamadi!");
            }


            var entityProperties = ObjectMapperApp.Mapper.Map<List<EntityProperty>>(input.EntityPropertyDtos);
            if (entityProperties.Count > 0 && docdef.DocumentEntityPropertys.Any())
            {
                foreach (var item in docdef.DocumentEntityPropertys)
                {
                    // İlgili özellik gereklilikleri karşılıyorsa ve kodu boşsa işlem yapılır
                    if (item.EntityProperty.Required && string.IsNullOrEmpty(item.EntityProperty.Code))
                    {
                        // entityProperties içinde ilgili kodu arar
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
                    Owner = input.Owner
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
                DocumentContent = ObjectMapperApp.Mapper.Map<DocumentContent>(input)
            };

            if(entityProperties.Any())
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
                var documentDys = new DocumentDysRequestModel(docdef.DocumentDys.ReferenceId.ToString(), docdef.Code, input.ToString(), input.FileType, fileByteArray);
                documentDys.DocumentParameters.Add("test", "test");
                await _dysProducer.PublishDysData(documentDys);
            }

            return true;
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

        public async Task<List<RootDocumentDto>> GetAllMethod(PagedInputDto pagedInputDto, CancellationToken cancellationToken)
        {
            var query = _dbContext!.Document;
            var response = new List<RootDocumentDto>();

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {
                response = mapToRootDocumentDto(securityQuestions);
            }

            return response;
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

        public async Task<ReleaseableFileStreamModel> DownloadDocument(DocumentDownloadInputDto inputDto, CancellationToken cancellationToken)
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
            return res;
        }
    }

    public interface IDocumentAppService
    {
        public Task<List<RootDocumentDto>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken);
        public Task<List<RootDocumentDto>> GetAllDocumentAll(CancellationToken cancellationToken);
        Task<bool> Instance(DocumentInstanceInputDto input);
        Task<ReleaseableFileStreamModel> DownloadDocument(DocumentDownloadInputDto inputDto, CancellationToken cancellationToken);
    }
}