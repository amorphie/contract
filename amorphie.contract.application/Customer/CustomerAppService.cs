using amorphie.contract.application.Customer.Dto;
using amorphie.contract.application.Customer.Request;
using amorphie.contract.application.Extensions;
using amorphie.contract.infrastructure.Contexts;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Elastic.Apm.Api;
using amorphie.contract.core.Services;
using Microsoft.Extensions.Configuration;
using amorphie.contract.core.Response;
using amorphie.contract.core;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document;

namespace amorphie.contract.application.Customer
{
    public interface ICustomerAppService
    {
        Task<GenericResult<Guid>> GetIdByReference(string userReference);
        Task<GenericResult<Guid>> AddAsync(CustomerInputDto inputDto);
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
        public async Task<GenericResult<Guid>> GetIdByReference(string userReference)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(userReference));

            var customerId = await _dbContext.Customer
                .Where(x => x.Reference == userReference)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (customerId == Guid.Empty)
            {
                return GenericResult<Guid>.Fail($"Customer not found. Reference: {userReference}");
            }

            return GenericResult<Guid>.Success(customerId);
        }
        public async Task<GenericResult<Guid>> AddAsync(CustomerInputDto inputDto)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(inputDto.Reference));

            var customer = new core.Entity.Customer
            {
                Reference = inputDto.Reference,
                Owner = inputDto.Owner,
                CustomerNo = inputDto.CustomerNo
            };

            _dbContext.Customer.Add(customer);

            await _dbContext.SaveChangesAsync();

            return GenericResult<Guid>.Success(customer.Id);
        }
        private async Task<List<DocumentForMinioObject>> GetDocuments(IQueryable<Document> documentQuery, CancellationToken token)
        {
            var documents = await documentQuery.Select(x => new DocumentForMinioObject
            {
                Id = x.Id,
                DocumentDefinitionId = x.DocumentDefinitionId,
                Status = x.Status,
                DocumentContentId = x.DocumentContent.Id.ToString()
            })
            .AsSplitQuery()
            .ToListAsync(token);

            return documents;
        }
        private void AddMatchedContract(List<CustomerContractDto> contractModels, List<CustomerContractDto> contractHistoryModels)
        {
            foreach (var contractHistoryModel in contractHistoryModels)
            {
                var matchingContract = contractModels.Find(x => x.Code == contractHistoryModel.Code);
                if (matchingContract != null)
                {
                    var firstDocumentDetail = contractHistoryModel.CustomerContractDocuments.FirstOrDefault();
                    if (firstDocumentDetail != null)
                    {
                        firstDocumentDetail.DocumentStatus = AppConsts.Expired;
                        matchingContract.CustomerContractDocuments.Add(firstDocumentDetail);
                    }
                    var firstGroupDetail = contractHistoryModel.CustomerContractDocumentGroups.FirstOrDefault();
                    if (firstGroupDetail != null)
                    {
                        firstGroupDetail.DocumentGroupStatus = AppConsts.Expired;
                        matchingContract.CustomerContractDocumentGroups.Add(firstGroupDetail);
                    }
                }
            }
        }
        private async Task<List<CustomerContractDto>> GetContractAndHistoryModels(IQueryable<ContractDefinition> allContractQuery, IQueryable<Document> documentQuery, GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token)
        {
            var history = await _dbContext.ContractDefinitionHistory
                .Where(x => allContractQuery.Select(ab => ab.Id).Contains(x.ContractDefinitionId))
                .Select(x => x.ContractDefinitionHistoryModel)
                .ToListAsync(token);

            var historyQuery = history.Where(x =>
                x.ContractDocumentDetails.Any(z => documentQuery.Select(d => d.DocumentDefinitionId).Contains(z.DocumentDefinitionId))
                || x.ContractDocumentGroupDetails.Any(z =>
                    z.DocumentGroup.DocumentGroupDetails.Any(y =>
                        documentQuery.Select(d => d.DocumentDefinitionId).Contains(y.DocumentDefinitionId))));

            var contractHistoryQuery = historyQuery.Where(x => x.BankEntity == inputDto.GetBankEntityCode());
            var contractHistory = ObjectMapperApp.Mapper.Map<List<ContractDefinition>>(contractHistoryQuery);

            var contractModels = await allContractQuery
                .Where(contract => contract.BankEntity == inputDto.GetBankEntityCode())
                .Where(contract =>
                    contract.ContractDocumentDetails.Any(cdd =>
                        documentQuery.Any(d => d.DocumentDefinitionId == cdd.DocumentDefinitionId)) ||
                    contract.ContractDocumentGroupDetails.Any(cdg =>
                        cdg.DocumentGroup.DocumentGroupDetails.Any(dgd =>
                            documentQuery.Any(d => d.DocumentDefinitionId == dgd.DocumentDefinitionId))))
                .AsNoTracking()
                .ProjectTo<CustomerContractDto>(ObjectMapperApp.Mapper.ConfigurationProvider)
                .ToListAsync(token);

            var contractHistoryModels = ObjectMapperApp.Mapper.Map<List<CustomerContractDto>>(contractHistory);

            AddMatchedContract(contractModels, contractHistoryModels);

            return contractModels;
        }
        public async Task<GenericResult<List<CustomerContractDto>>> GetDocumentsByContracts(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token)
        {
            bool othersOnly = inputDto.Code == "//OtherDocuments";
            if (othersOnly)
                inputDto.Code = "";

            var userReference = inputDto.GetUserReference();

            var documentQuery = _dbContext.Document.Where(x => x.Customer.Reference == userReference);
            var allContractQuery = _dbContext.ContractDefinition;

            ContractHelperExtensions.LikeWhere(allContractQuery, inputDto.Code);

            if (inputDto.StartDate.HasValue)
                documentQuery = documentQuery.Where(x => x.CreatedAt > inputDto.StartDate.Value);

            if (inputDto.EndDate.HasValue)
                documentQuery = documentQuery.Where(x => x.CreatedAt < inputDto.EndDate.Value);

            var documents = await GetDocuments(documentQuery, token);
            var contractModels = await GetContractAndHistoryModels(allContractQuery, documentQuery, inputDto, token);
            var allContractDocumentIds = GetAllContractDocumentIds(allContractQuery);
            ProcessContractModels(inputDto, documents, contractModels);

            if (String.IsNullOrEmpty(inputDto.Code))
            {
                allContractDocumentIds = allContractDocumentIds.Distinct().ToList();
                CustomerContractDto contractModel = GetOtherDocuments(inputDto, documents, allContractDocumentIds);
                GetMinioUrl(contractModel, documents);

                if (othersOnly)
                    contractModels = new List<CustomerContractDto> { contractModel };
                else
                    contractModels.Add(contractModel);
            }
            return GenericResult<List<CustomerContractDto>>.Success(contractModels);
        }
        private void ProcessContractModels(GetCustomerDocumentsByContractInputDto inputDto, List<DocumentForMinioObject> documents, List<CustomerContractDto> contractModels)
        {
            foreach (var model in contractModels)
            {
                Elastic.Apm.Agent.Tracer.CurrentTransaction.CaptureSpan("For.ContractDocuments", ApiConstants.ActionExec, () =>
                {
                    model.Title = model.Titles.L(inputDto.GetLanguageCode());

                    var customerCompletedDocuments = documents.Where(x => x.Status == core.Enum.EStatus.Completed).ToList();
                    var customerNotCompletedDocuments = documents.Where(x => x.Status != core.Enum.EStatus.Completed).ToList();

                    UpdateContractDocuments(inputDto, model, model.CustomerContractDocuments, customerCompletedDocuments, customerNotCompletedDocuments);
                    UpdateContractDocumentGroups(inputDto, model, model.CustomerContractDocumentGroups, customerCompletedDocuments, customerNotCompletedDocuments);

                    var anyNotValidDocument = model.CustomerContractDocuments.Exists(x => x.Required && x.DocumentStatus != AppConsts.Valid);
                    var anyNotValidDocumentGroup = model.CustomerContractDocumentGroups.Exists(x => x.Required && x.DocumentGroupStatus != AppConsts.Valid);

                    model.ContractStatus = anyNotValidDocument || anyNotValidDocumentGroup ? AppConsts.InProgress : AppConsts.Valid;

                    if (model.ContractStatus == AppConsts.Valid || model.ContractStatus == AppConsts.InProgress || model.ContractStatus == AppConsts.Expired)
                    {
                        GetMinioUrl(model, documents);
                    }
                });
            }
        }

        private List<Guid> GetAllContractDocumentIds(IQueryable<ContractDefinition> allContractQuery)
        {
            return allContractQuery
                .SelectMany(main => main.ContractDocumentDetails.Select(doc => doc.DocumentDefinitionId))
                .Concat(allContractQuery.SelectMany(main => main.ContractDocumentGroupDetails.Select(docGrup => docGrup.DocumentGroupId)))
                .ToList();
        }

        private CustomerContractDto GetOtherDocuments(GetCustomerDocumentsByContractInputDto inputDto, List<DocumentForMinioObject> documents, List<Guid> allContractDocumentIds)
        {
            var otherDocuments = documents.Where(x => !allContractDocumentIds.Contains(x.DocumentDefinitionId)).ToList();
            var otherDocumentDefinition = _dbContext.DocumentDefinition
                .IgnoreQueryFilters()
                .Where(x => otherDocuments.Select(x => x.DocumentDefinitionId).Contains(x.Id))
                .AsNoTracking()
                .AsSplitQuery()
                .ProjectTo<CustomerContractDocumentDto>(ObjectMapperApp.Mapper.ConfigurationProvider)
                .ToList();

            CustomerContractDto contractModel = new CustomerContractDto
            {
                Code = "Other_idle_docs",
                Title = inputDto.GetLanguageCode() == "tr-TR" ? "Diğer" : "Other",
                ContractStatus = "",
                CustomerContractDocuments = otherDocumentDefinition,
                CustomerContractDocumentGroups = new List<CustomerContractDocumentGroupDto>()
            };

            return contractModel;
        }

        private void UpdateContractDocuments(GetCustomerDocumentsByContractInputDto inputDto, CustomerContractDto customerContractDto, List<CustomerContractDocumentDto> contractDocuments, List<DocumentForMinioObject> customerCompletedDocuments, List<DocumentForMinioObject> customerNotCompletedDocuments)
        {
            foreach (var contDocument in contractDocuments)
            {
                contDocument.Title = contDocument.Titles.L(inputDto.GetLanguageCode());

                if (customerCompletedDocuments.Exists(x => contDocument.Id == x.DocumentDefinitionId) && contDocument.DocumentStatus != AppConsts.Expired)
                {
                    contDocument.DocumentStatus = AppConsts.Valid;
                    customerContractDto.ContractStatus = AppConsts.InProgress;
                }
                else if (customerNotCompletedDocuments.Exists(x => contDocument.Id == x.DocumentDefinitionId))
                {
                    contDocument.DocumentStatus = AppConsts.InProgress;
                }
            }
        }

        private void UpdateContractDocumentGroups(GetCustomerDocumentsByContractInputDto inputDto, CustomerContractDto customerContractDto, List<CustomerContractDocumentGroupDto> contractDocumentGroups, List<DocumentForMinioObject> customerCompletedDocuments, List<DocumentForMinioObject> customerNotCompletedDocuments)
        {
            foreach (var contractDocGroup in contractDocumentGroups)
            {
                contractDocGroup.Title = contractDocGroup.Titles.L(inputDto.GetLanguageCode());
                int validDocCount = 0;

                foreach (var groupDocument in contractDocGroup.CustomerContractGroupDocuments)
                {
                    groupDocument.Title = groupDocument.Titles.L(inputDto.GetLanguageCode());

                    if (customerCompletedDocuments.Exists(x => groupDocument.Id == x.DocumentDefinitionId) && groupDocument.DocumentStatus != AppConsts.Expired)
                    {
                        groupDocument.DocumentStatus = AppConsts.Valid;
                        customerContractDto.ContractStatus = AppConsts.InProgress;
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
        }
        private void GetMinioUrl(CustomerContractDto model, List<DocumentForMinioObject> documents)
        {
            var minioDocuments = model.CustomerContractDocuments
                .Where(x => x.DocumentStatus == AppConsts.InProgress || x.DocumentStatus == AppConsts.Valid || x.DocumentStatus == AppConsts.Expired)
                .Select(x => new { DocumentId = x.Id, DocumentDefinitionId = x.Id })
                .Concat(model.CustomerContractDocumentGroups
                    .SelectMany(x => x.CustomerContractGroupDocuments
                        .Where(y => y.DocumentStatus == AppConsts.InProgress || y.DocumentStatus == AppConsts.Valid || y.DocumentStatus == AppConsts.Expired)
                        .Select(y => new { DocumentId = y.Id, DocumentDefinitionId = y.Id })))
                .Distinct()
                .ToList();

            foreach (var minioDoc in minioDocuments)
            {
                var documentDefinition = documents.Find(z => z.DocumentDefinitionId == minioDoc.DocumentDefinitionId);
                if (documentDefinition != null)
                {
                    var minioUrl = $"{_baseUrl}{_downloadEndpoint}?ObjectId={documentDefinition.DocumentContentId}";
                    var contractDoc = model.CustomerContractDocuments.Find(x => x.Id == minioDoc.DocumentId);
                    if (contractDoc != null)
                        contractDoc.MinioUrl = minioUrl;
                    else
                    {
                        var groupDoc = model.CustomerContractDocumentGroups.SelectMany(x => x.CustomerContractGroupDocuments).FirstOrDefault(x => x.Id == minioDoc.DocumentId);
                        if (groupDoc != null)
                            groupDoc.MinioUrl = minioUrl;
                    }
                }
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

