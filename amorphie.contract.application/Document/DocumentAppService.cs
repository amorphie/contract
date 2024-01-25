using amorphie.contract.core.Entity;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Services;
using amorphie.contract.data.Contexts;
using amorphie.core.Base;
using amorphie.core.Enums;
using Microsoft.EntityFrameworkCore;


namespace amorphie.contract.application
{

    public class DocumentAppService : IDocumentAppService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IMinioService _minioService;

        public DocumentAppService(ProjectDbContext projectDbContext, IMinioService minioService)
        {
            _dbContext = projectDbContext;
            _minioService = minioService;
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

        public async Task<Result> Instance(DocumentInstanceInputDto input)
        {

            var docdef = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == input.DocumentCode && x.Semver == input.DocumentVersion);
            if (docdef == null)
            {
                return new Result(amorphie.core.Enums.Status.Error, "Document Code ve versiyona ait kayit bulunamadi!");
            }

            var cus = _dbContext.Customer.FirstOrDefault(x => x.Reference == input.Reference);
            if (cus == null)
            {
                cus = new Customer
                {
                    Reference = input.Reference,
                    Owner = input.Owner
                };

                _dbContext.Customer.Add(cus);
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
            else
            {
                fileByteArray = Convert.FromBase64String(input.FileContext);
            }

            await _minioService.UploadFile(fileByteArray, input.ToString(), input.FileType, "");

            return new Result(Status.Success, "OK");
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

        private List<RootDocumentDto> mapToRootDocumentDto(List<core.Entity.Document.Document> documents)
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
    }

    public interface IDocumentAppService
    {
        public Task<List<RootDocumentDto>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken);
        public Task<List<RootDocumentDto>> GetAllDocumentAll(CancellationToken cancellationToken);
        Task<Result> Instance(DocumentInstanceInputDto input);
    }
}