using amorphie.contract.application.Customer.Dto;
using amorphie.contract.application.Customer.Request;
using amorphie.contract.application.Extensions;
using amorphie.contract.infrastructure.Contexts;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Elastic.Apm.Api;
using amorphie.core.Base;
using amorphie.contract.core.Services;
using Microsoft.Extensions.Configuration;
using amorphie.contract.core.Response;
using amorphie.contract.core;
using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Customer
{
    public interface ICustomerAppService
    {
        Task<GenericResult<List<CustomerContractDto>>> GetDocumentsByContracts(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token);
        Task<GenericResult<List<DocumentCustomerDto>>> GetAllDocuments(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token);
        Task<GenericResult<bool>> DeleteAllDocuments(string reference, CancellationToken cts);
    }

    public partial class CustomerAppService : ICustomerAppService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IMinioService _minioService;

        private readonly string _baseUrl;
        private readonly string _downloadEndpoint;


        public CustomerAppService(ProjectDbContext dbContext, IMinioService minioService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _minioService = minioService;

            _baseUrl = StaticValuesExtensions.Apisix.BaseUrl;
            _downloadEndpoint = StaticValuesExtensions.Apisix.DownloadEndpoint;
        }

        public string FindTitle(List<MultilanguageText> texts, string language)
        {
            if (!texts.Any())
                return "Undefined title";

            var lang = texts.FirstOrDefault(x => x.Language == language);

            return lang != null ?
                lang.Label :
                texts.FirstOrDefault().Label;
        }

        public async Task<GenericResult<List<CustomerContractDto>>> GetDocumentsByContracts(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token)
        {
            bool othersOnly = false;
            if (inputDto.Code == "//OtherDocuments")
            {
                inputDto.Code = "";
                othersOnly = true;
            }

            var userReference = inputDto.GetUserReference();

            var documentQuery = _dbContext.Document.Where(x => x.Customer.Reference == userReference).AsQueryable();
            var contractQuery = _dbContext!.ContractDefinition.Where(x => x.BankEntity == inputDto.GetBankEntityCode()).AsQueryable();
            contractQuery = ContractHelperExtensions.LikeWhere(contractQuery, inputDto.Code);


            if (inputDto.StartDate.HasValue)
            {
                documentQuery = documentQuery.Where(x => x.CreatedAt > inputDto.StartDate.Value);
            }

            if (inputDto.EndDate.HasValue)
            {
                documentQuery = documentQuery.Where(x => x.CreatedAt < inputDto.EndDate.Value);
            }

            var documents = await documentQuery.Select(x => new DocumentForMinioObject
            {
                Id = x.Id,
                DocumentDefinitionId = x.DocumentDefinitionId,
                Status = x.Status,
                DocumentContentId = x.DocumentContent.Id.ToString()
            })
            .AsSplitQuery()
            .ToListAsync(token);

            contractQuery = contractQuery.Where(x => x.ContractDocumentDetails.Any(z => documents.Select(d => d.DocumentDefinitionId).Contains(z.DocumentDefinitionId))
            ||
            x.ContractDocumentGroupDetails.Any(z => z.DocumentGroup.DocumentGroupDetails.Any(y => documents.Select(d => d.DocumentDefinitionId).Contains(y.DocumentDefinitionId))));

            var contractModels = await contractQuery.AsNoTracking().AsSplitQuery().ProjectTo<CustomerContractDto>(ObjectMapperApp.Mapper.ConfigurationProvider).ToListAsync(token);

            List<Guid> allContractDocumentIds = new List<Guid>();
            foreach (var model in contractModels)
            {
                allContractDocumentIds.AddRange(model.CustomerContractDocuments.Select(x => x.Id));

                foreach (var group in model.CustomerContractDocumentGroups)
                {
                    allContractDocumentIds.AddRange(group.CustomerContractGroupDocuments.Select(x => x.Id));
                }

                model.Title = FindTitle(model.MultiLanguageText, inputDto.GetLanguageCode());

                var contractDocuments = model.CustomerContractDocuments;
                var contractDocumentGroups = model.CustomerContractDocumentGroups;

                var customerCompletedDocuments = documents.Where(x => x.Status == core.Enum.EStatus.Completed).ToList();
                var customerNotCompletedDocuments = documents.Where(x => x.Status != core.Enum.EStatus.Completed).ToList();

                foreach (var contDocument in contractDocuments)
                {
                    contDocument.Title = FindTitle(contDocument.MultiLanguageText, inputDto.GetLanguageCode());
                    if (customerCompletedDocuments.Exists(x => contDocument.Id == x.DocumentDefinitionId))
                    {
                        contDocument.DocumentStatus = AppConsts.Valid;
                        model.ContractStatus = AppConsts.InProgress;
                    }
                    else if (customerNotCompletedDocuments.Exists(x => contDocument.Id == x.DocumentDefinitionId))
                    {
                        contDocument.DocumentStatus = AppConsts.InProgress;
                    }
                }

                Elastic.Apm.Agent.Tracer.CurrentTransaction.CaptureSpan("For.contractDocumentGroups", ApiConstants.ActionExec, ()
                    =>
                 {
                     foreach (var contractDocGroup in contractDocumentGroups)
                     {
                         contractDocGroup.Title = FindTitle(contractDocGroup.MultiLanguageText, inputDto.GetLanguageCode());

                         int validDocCount = 0;

                         foreach (var groupDocument in contractDocGroup.CustomerContractGroupDocuments)
                         {
                             groupDocument.Title = FindTitle(groupDocument.MultiLanguageText, inputDto.GetLanguageCode());

                             if (customerCompletedDocuments.Exists(x => groupDocument.Id == x.DocumentDefinitionId))
                             {
                                 groupDocument.DocumentStatus = AppConsts.Valid;
                                 model.ContractStatus = AppConsts.InProgress;
                                 validDocCount++;
                             }
                             else if (customerNotCompletedDocuments.Exists(x => groupDocument.Id == x.DocumentDefinitionId))
                             {
                                 groupDocument.DocumentStatus = AppConsts.InProgress;
                             }
                         }

                         if (contractDocGroup.AtLeastRequiredDocument <= validDocCount)
                         {
                             contractDocGroup.DocumentGroupStatus = AppConsts.Valid;
                         }
                         else if (validDocCount > 0 && validDocCount < contractDocGroup.AtLeastRequiredDocument)
                         {
                             contractDocGroup.DocumentGroupStatus = AppConsts.InProgress;
                         }
                     }
                 });
                bool anyNotValidDocument = model.CustomerContractDocuments.Any(x => x.Required && x.DocumentStatus != AppConsts.Valid);
                bool anyNotValidDocumentGroup = model.CustomerContractDocumentGroups.Any(x => x.Required && x.DocumentGroupStatus != AppConsts.Valid);

                if (!anyNotValidDocument && !anyNotValidDocumentGroup)
                {
                    model.ContractStatus = AppConsts.Valid;
                }

                Elastic.Apm.Agent.Tracer.CurrentTransaction.CaptureSpan("If.Valid.InProgress", ApiConstants.ActionExec, ()
                    =>
                 {
                     if (model.ContractStatus == AppConsts.Valid || model.ContractStatus == AppConsts.InProgress)
                     {
                         GetMinioUrl(model, documents, token);
                     }
                 });
            }

            if (String.IsNullOrEmpty(inputDto.Code))
            {
                allContractDocumentIds = allContractDocumentIds.Distinct().ToList();

                var otherDocuments = documents.Where(x => !allContractDocumentIds.Contains(x.DocumentDefinitionId));

                var otherDocumentDefinition = _dbContext.DocumentDefinition
                                                .IgnoreQueryFilters()
                                                .Where(x => otherDocuments.Select(x => x.DocumentDefinitionId).Contains(x.Id)).AsNoTracking().AsSplitQuery().ProjectTo<CustomerContractDocumentDto>(ObjectMapperApp.Mapper.ConfigurationProvider).ToList();

                otherDocumentDefinition.ForEach(x =>
                {
                    x.Title = FindTitle(x.MultiLanguageText, inputDto.GetLanguageCode());
                    x.DocumentStatus = AppConsts.Valid;
                });

                CustomerContractDto contractModel = new CustomerContractDto
                {
                    Code = "Other_idle_docs",
                    Title = inputDto.GetLanguageCode() == "tr-TR" ? "Diğer" : "Other",
                    ContractStatus = "",
                    CustomerContractDocuments = otherDocumentDefinition,
                    CustomerContractDocumentGroups = new List<CustomerContractDocumentGroupDto>()
                };

                GetMinioUrl(contractModel, documents, token);

                if (othersOnly)
                {
                    contractModels = new List<CustomerContractDto>
                    {
                        contractModel
                    };
                }
                else
                {
                    contractModels.Add(contractModel);
                }
            }

            return GenericResult<List<CustomerContractDto>>.Success(contractModels);
        }


        private void GetMinioUrl(CustomerContractDto model, List<DocumentForMinioObject> documents, CancellationToken token)
        {
            List<MinioObject> minioDocuments = new List<MinioObject>();

            if (model.Code == "Other_idle_docs")
            {
                minioDocuments = model.CustomerContractDocuments.Select(x => new MinioObject
                {
                    DocumentDefinitionId = x.Id,
                    MinioUrl = ""
                }).ToList();
            }
            else
            {
                minioDocuments = model.CustomerContractDocuments.Where(x => x.DocumentStatus == AppConsts.InProgress || x.DocumentStatus == AppConsts.Valid).Select(x => new MinioObject
                {
                    DocumentDefinitionId = x.Id,
                    MinioUrl = ""
                }).ToList();

                model.CustomerContractDocumentGroups.ForEach(x =>
                {
                    minioDocuments.AddRange(x.CustomerContractGroupDocuments.Where(x => x.DocumentStatus == AppConsts.InProgress || x.DocumentStatus == AppConsts.Valid).Select(x => new MinioObject
                    {
                        DocumentDefinitionId = x.Id,
                        MinioUrl = ""
                    }).ToList());
                });

                minioDocuments = minioDocuments.GroupBy(x => x.DocumentDefinitionId).Select(x => x.First()).ToList();
            }


            foreach (var minioDoc in minioDocuments)
            {
                var documentDefinition = documents.FirstOrDefault(z => z.DocumentDefinitionId == minioDoc.DocumentDefinitionId);
                if (documentDefinition != null)
                {
                    string minioUrl = $"{_baseUrl}{_downloadEndpoint}?ObjectName={documentDefinition.DocumentContentId}";

                    minioDoc.MinioUrl = minioUrl;
                }
            }

            model.CustomerContractDocuments.Where(x => minioDocuments.Select(z => z.DocumentDefinitionId).Contains(x.Id)).ToList().ForEach(x => x.MinioUrl = minioDocuments.FirstOrDefault(z => z.DocumentDefinitionId == x.Id).MinioUrl);

            foreach (var contractGroup in model.CustomerContractDocumentGroups)
            {
                contractGroup.CustomerContractGroupDocuments.Where(x => minioDocuments.Select(z => z.DocumentDefinitionId).Contains(x.Id)).ToList().ForEach(x => x.MinioUrl = minioDocuments.FirstOrDefault(z => z.DocumentDefinitionId == x.Id).MinioUrl);
            }
        }
        public async Task<GenericResult<List<DocumentCustomerDto>>> GetAllDocuments(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token)
        {
            var documentsQuery = _dbContext!.Document.Where(x => x.Customer.Reference == inputDto.GetUserReference()).AsQueryable();

            if (inputDto.StartDate.HasValue)
            {
                documentsQuery = documentsQuery.Where(x => x.CreatedAt > inputDto.StartDate.Value);
            }

            if (inputDto.EndDate.HasValue)
            {
                documentsQuery = documentsQuery.Where(x => x.CreatedAt < inputDto.EndDate.Value);
            }

            var documents = await documentsQuery.ToListAsync(token);

            var responseTasks = documents.Select(async x =>
            {
                var minioUrl = await _minioService.GetDocumentUrl(x.DocumentContent.MinioObjectName, token);
                return new DocumentCustomerDto
                {
                    Code = x.DocumentDefinition.Code,
                    Semver = x.DocumentDefinition.Semver,
                    Status = x.Status.ToString(),
                    MinioUrl = minioUrl,
                    MinioObjectName = x.DocumentContent.MinioObjectName,
                    Reference = x.Customer.Reference
                };
            });
            var response = await Task.WhenAll(responseTasks);
            return GenericResult<List<DocumentCustomerDto>>.Success(response.ToList());
        }

        public async Task<GenericResult<bool>> DeleteAllDocuments(string reference, CancellationToken cts)
        {

            var customerIdsToDelete = await _dbContext.Customer
                                    .Where(c => c.Reference == reference)
                                    .Select(c => c.Id)
                                    .ToListAsync();

            var documentsToDelete = await _dbContext.Document
                                    .Where(d => customerIdsToDelete.Contains(d.CustomerId))
                                    .ToListAsync();

            _dbContext.Document.RemoveRange(documentsToDelete);
            await _dbContext.SaveChangesAsync();
            return GenericResult<bool>.Success(true);


        }

        private class MinioObject
        {
            public string MinioUrl { get; set; }
            public Guid DocumentDefinitionId { get; set; }
        }

        private class DocumentForMinioObject
        {
            public Guid Id { get; set; }
            public Guid DocumentDefinitionId { get; set; }
            public EStatus Status { get; set; }
            public string DocumentContentId { get; set; }
        }
    }
}

