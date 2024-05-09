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
using amorphie.contract.core.Model.Documents;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core.Entity.Document.DocumentTypes;
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

        public async Task<List<CustomerContractDto>> MapToCustomerContract(
                List<CustomerContractDto> customerContract, 
                IQueryable<Document> documentQuery,
                List<CustomerDocumentDto> customerDocument,
                GetCustomerDocumentsByContractInputDto inputDto, 
                bool othersOnly = false)
        {
            List<CustomerContractDto> customerContractDtos = new List<CustomerContractDto>();
            List<Guid> allContractDocumentIds = new List<Guid>();

            foreach (var contract in customerContract)
            {
                CustomerContractDto customerContractDto = new CustomerContractDto();
                customerContractDto.Id = contract.Id;
                customerContractDto.Code = contract.Code;
                customerContractDto.Titles = contract.Titles;
                customerContractDto.Title = contract.Titles.L(inputDto.GetLanguageCode());

                if(contract.CustomerContractDocuments != null)
                {

                    List<CustomerContractDocumentDto> customerContractDocumentDtos = new List<CustomerContractDocumentDto>();
                    foreach (var contractDocument in contract.CustomerContractDocuments)
                    {
                        var findDocumentVersion = contract.CustomerContractDocuments.Where(x => x.Code == contractDocument.Code).Select(x=>x.Version).ToArray();
                        var longestVersion = Versioning.FindHighestVersion(findDocumentVersion);
                        var document = customerDocument.Where(d => d.DocumentDefinitionId == contractDocument.Id).FirstOrDefault();
                        if(document == null && contractDocument.IsDeleted)
                            continue;
                        CustomerContractDocumentDto customerContractDocumentDto = new CustomerContractDocumentDto();
                        customerContractDocumentDto.Id = contractDocument.Id;
                        customerContractDocumentDto.Title = contractDocument.Titles.L(inputDto.GetLanguageCode());
                        customerContractDocumentDto.Code = contractDocument.Code;
                        if(document?.Status == ApprovalStatus.Approved && contractDocument.Version != longestVersion)
                            customerContractDocumentDto.DocumentStatus = AppConsts.Expired;
                        else if(document?.Status == ApprovalStatus.Approved )
                            customerContractDocumentDto.DocumentStatus = AppConsts.Valid;
                        else
                            customerContractDocumentDto.DocumentStatus = AppConsts.InProgress;
                        customerContractDocumentDto.Required = contractDocument.Required;
                        customerContractDocumentDto.Render = contractDocument.Render;
                        customerContractDocumentDto.Version = contractDocument.Version;
                        customerContractDocumentDto.MinioUrl = document?.DocumentContentId == null ? null : $"{_baseUrl}{_downloadEndpoint}?ObjectId={document.DocumentContentId}";
                        customerContractDocumentDto.ApprovalDate = document?.CreatedAt == null ? DateTime.MinValue : document.CreatedAt;
                        customerContractDocumentDto.OnlineSign = contractDocument.OnlineSign;
                        allContractDocumentIds.Add(contractDocument.Id);
                        customerContractDocumentDtos.Add(customerContractDocumentDto);
                    }
                    customerContractDto.CustomerContractDocuments = customerContractDocumentDtos;
                }
                if(contract.CustomerContractDocumentGroups != null)
                {
                    List<CustomerContractDocumentGroupDto> customerContractDocumentGroupDtos = new List<CustomerContractDocumentGroupDto>();
                    foreach (var contractDocumentGroup in contract.CustomerContractDocumentGroups)
                    {
                        CustomerContractDocumentGroupDto customerContractDocumentGroupDto = new CustomerContractDocumentGroupDto();
                        List<CustomerContractDocumentDto> customerContractDocumentDtos = new List<CustomerContractDocumentDto>();
                        customerContractDocumentGroupDto.Id = contractDocumentGroup.Id;
                        customerContractDocumentGroupDto.Code = contractDocumentGroup.Code;
                        customerContractDocumentGroupDto.CustomerContractGroupDocuments = new List<CustomerContractDocumentDto>();
                        foreach (var contractDocumentGroupDocument in contractDocumentGroup.CustomerContractGroupDocuments)
                        {
                            var findDocumentVersion = contractDocumentGroup.CustomerContractGroupDocuments.Where(x => x.Code == contractDocumentGroupDocument.Code).Select(x=>x.Version).ToArray();
                            var longestVersion = Versioning.FindHighestVersion(findDocumentVersion);
                            var document = customerDocument.Where(d => d.DocumentDefinitionId == contractDocumentGroupDocument.Id).FirstOrDefault();
                            CustomerContractDocumentDto customerContractDocumentDto = new CustomerContractDocumentDto();
                            customerContractDocumentDto.Id = contractDocumentGroupDocument.Id;
                            customerContractDocumentDto.Title = contractDocumentGroupDocument.Titles.L(inputDto.GetLanguageCode());
                            customerContractDocumentDto.Code = contractDocumentGroupDocument.Code;
                            if(document?.Status == ApprovalStatus.Approved && contractDocumentGroupDocument.Version != longestVersion)
                                customerContractDocumentDto.DocumentStatus = AppConsts.Expired;
                            else if(document?.Status == ApprovalStatus.Approved )
                                customerContractDocumentDto.DocumentStatus = AppConsts.Valid;
                            else
                                customerContractDocumentDto.DocumentStatus = AppConsts.InProgress;
                            customerContractDocumentDto.Render = contractDocumentGroupDocument.Render;
                            customerContractDocumentDto.Version = contractDocumentGroupDocument.Version;
                            customerContractDocumentDto.MinioUrl = document?.DocumentContentId == null ? null : $"{_baseUrl}{_downloadEndpoint}?ObjectId={document.DocumentContentId}";
                            customerContractDocumentDto.ApprovalDate = document?.CreatedAt == null ? DateTime.MinValue : document.CreatedAt;
                            customerContractDocumentDto.OnlineSign = contractDocumentGroupDocument.OnlineSign;
                            allContractDocumentIds.Add(contractDocumentGroupDocument.Id);
                            customerContractDocumentDtos.Add(customerContractDocumentDto);
                        }
                        customerContractDocumentGroupDto.AtLeastRequiredDocument = contractDocumentGroup.AtLeastRequiredDocument;
                        customerContractDocumentGroupDto.Required = contractDocumentGroup.Required;
                        //customerContractDocumentDtos daki Tüm dökümanlar Valid mi kontrolü
                        var allValid = customerContractDocumentDtos.All(x => x.DocumentStatus == AppConsts.Valid);
                        customerContractDocumentGroupDto.DocumentGroupStatus = allValid?AppConsts.Valid: AppConsts.NotValid;
                        customerContractDocumentGroupDto.Titles = contractDocumentGroup.Titles;
                        customerContractDocumentGroupDto.CustomerContractGroupDocuments = customerContractDocumentDtos;
                        customerContractDocumentGroupDtos.Add(customerContractDocumentGroupDto);
                    }
                    customerContractDto.CustomerContractDocumentGroups = customerContractDocumentGroupDtos;
                }
                customerContractDtos.Add(customerContractDto);
                
            }
            var otherContract = await GetOtherDocuments(documentQuery,allContractDocumentIds, inputDto);                         
            customerContractDtos.Add(otherContract);
            if(othersOnly)
            {
                customerContractDtos = new List<CustomerContractDto> { otherContract };
            }
            return customerContractDtos;
        }
        public async Task<CustomerContractDto> GetOtherDocuments(IQueryable<Document> documentQuery, List<Guid> allContractDocumentIds,GetCustomerDocumentsByContractInputDto inputDto)
        {
            var otherDocuments = documentQuery.Where(d=>!allContractDocumentIds.Contains(d.DocumentDefinitionId))
                    .Select(d=> new CustomerContractDocumentDto
                        {
                            Id = d.DocumentDefinitionId,
                            Title = d.DocumentDefinition.Titles.L(inputDto.GetLanguageCode()),
                            Code = d.DocumentDefinition.Code,
                            DocumentStatus = d.Status == ApprovalStatus.Approved ? AppConsts.Valid : AppConsts.InProgress,
                            Required = true,
                            Render = d.DocumentDefinition.DocumentOnlineSign != null,
                            Version = d.DocumentDefinition.Semver,
                            MinioUrl = $"{_baseUrl}{_downloadEndpoint}?ObjectId={d.DocumentContentId}",
                            ApprovalDate = d.CreatedAt,
                            OnlineSign = new OnlineSignDto
                            {
                                AllovedClients = d.DocumentDefinition.DocumentOnlineSign.DocumentAllowedClientDetails.Select(x => x.DocumentAllowedClients.Code).ToList(),
                                ScaRequired = d.DocumentDefinition.DocumentOnlineSign.Required,
                                DocumentModelTemplate = d.DocumentDefinition.DocumentOnlineSign.Templates.Select(x => new DocumentTemplateDto
                                {
                                    Name = x.Code,
                                    MinVersion = x.Version
                                }).ToList()
                            }
                        }).ToList();
            var otherContract = new CustomerContractDto
            {
                Code = "//OtherDocuments",
                Titles = new Dictionary<string, string> { { "tr", "Diğer Belgeler" }, { "en", "Other Documents" } },
                Title = inputDto.GetLanguageCode() == "tr-TR" ? "Diğer" : "Other",
                CustomerContractDocuments = otherDocuments
            };
            return otherContract;
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

            var contractQuery = _dbContext!.ContractDefinition.AsQueryable();
            contractQuery = ContractHelperExtensions.LikeWhere(contractQuery, inputDto.Code);

            if (inputDto.StartDate.HasValue)
                documentQuery = documentQuery.Where(x => x.CreatedAt >= inputDto.StartDate.Value);
            if (inputDto.EndDate.HasValue)
                documentQuery = documentQuery.Where(x => x.CreatedAt <= inputDto.EndDate.Value);
            var userId = _dbContext.Customer.Where(x => x.Reference == userReference).Select(x => x.Id).FirstOrDefault();
            
            var querableDocument = _dbContext.Document.Where(x => x.Customer.Reference == userReference).AsQueryable();

            List<CustomerDocumentDto> customDocument = querableDocument
                    .Select(d => new CustomerDocumentDto
                    {
                        Id= d.Id,
                        DocumentDefinitionId= d.DocumentDefinitionId,
                        Status=d.Status,
                        DocumentContentId= d.DocumentContentId,
                        CreatedAt=d.CreatedAt
                    }).ToList();

            var querableDocumentIds = customDocument.Select(d => d.DocumentDefinitionId).ToList();
            List<CustomerContractDto> quearableContract = contractQuery.IgnoreAutoIncludes().IgnoreQueryFilters()
                .Where(c => 
                            (
                                c.ContractDocumentDetails.Any(cdd => querableDocumentIds.Contains(cdd.DocumentDefinitionId)) 
                                && 
                                _dbContext.UserSignedContract.Any(uc => uc.ContractCode == c.Code && uc.CustomerId == userId)
                            )
                            || 
                                c.ContractDocumentGroupDetails.Any(cdgd => cdgd.DocumentGroup.DocumentGroupDetails.Any(dgd => querableDocumentIds.Contains(dgd.DocumentDefinitionId)) 
                                && 
                                _dbContext.UserSignedContract.Any(uc => uc.ContractCode == c.Code && uc.CustomerId == userId)
                            )
                     )
                .Select(cd => new CustomerContractDto
                {
                    Id=cd.Id,
                    Code = cd.Code,
                    Titles= cd.Titles,
                    Status= cd.Status,
                    IsDeleted = cd.IsDeleted,
                    CustomerContractDocumentGroups = cd.ContractDocumentGroupDetails
                        .Select(cdgd => new CustomerContractDocumentGroupDto
                        {
                            Id = cdgd.DocumentGroupId,
                            Code= cdgd.DocumentGroup.Code,
                            Titles=cdgd.DocumentGroup.Titles,
                            AtLeastRequiredDocument= cdgd.AtLeastRequiredDocument,
                            Required= cdgd.Required,
                            CustomerContractGroupDocuments = cdgd.DocumentGroup.DocumentGroupDetails.Select(dgd => new CustomerContractDocumentDto
                            {
                                Id=dgd.DocumentDefinitionId,
                                Titles=dgd.DocumentDefinition.Titles,
                                Code=dgd.DocumentDefinition.Code,
                                Required=cdgd.Required,
                                Render = dgd.DocumentDefinition.DocumentOnlineSign != null,
                                Version=dgd.DocumentDefinition.Semver,
                                OnlineSign= ObjectMapperApp.Mapper.Map<OnlineSignDto>(dgd.DocumentDefinition.DocumentOnlineSign),
                                IsDeleted=dgd.IsDeleted
                                })
                        }),
                    CustomerContractDocuments = cd.ContractDocumentDetails
                        .Select(cdd => new CustomerContractDocumentDto
                        {
                            Id=cdd.DocumentDefinitionId,
                            Titles= cdd.DocumentDefinition.Titles,
                            Code=cdd.DocumentDefinition.Code,
                            Required=cdd.Required,
                            Render = cdd.DocumentDefinition.DocumentOnlineSign != null,
                            Version=cdd.DocumentDefinition.Semver,
                            OnlineSign=ObjectMapperApp.Mapper.Map<OnlineSignDto>(cdd.DocumentDefinition.DocumentOnlineSign),
                            IsDeleted=cdd.IsDeleted

                        }),
                })
                .ToList();

            var customerContract = await MapToCustomerContract(quearableContract, documentQuery, customDocument, inputDto, othersOnly);
            
            return GenericResult<List<CustomerContractDto>>.Success(customerContract);
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
    }
}

