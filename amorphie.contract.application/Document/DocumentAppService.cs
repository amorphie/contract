using System.Data;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.core;
using amorphie.contract.core.Entity;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Services;
using amorphie.contract.data.Contexts;
using amorphie.contract.data.Extensions;
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

        public DocumentAppService(ProjectDbContext projectDbContext, IMinioService minioService)
        {
            _dbContext = projectDbContext;
            _minioService = minioService;
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


        public async Task<GenericResult<List<RootDocumentDto>>>GetAllDocumentAll(CancellationToken cancellationToken)
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

        public async Task<GenericResult<bool>> Instance(DocumentInstanceInputDto input)
        {

            var docdef = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == input.DocumentCode && x.Semver == input.DocumentVersion);
            if (docdef == null)
            {
                throw new ArgumentException("Document Code ve versiyona ait kayit bulunamadi!");
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

            _dbContext.Document.Add(document);
            _dbContext.SaveChanges();

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

            return GenericResult<bool>.Success(true);
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