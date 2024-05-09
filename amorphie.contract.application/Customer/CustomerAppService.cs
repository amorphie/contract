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
                documentQuery = documentQuery.Where(x => x.CreatedAt > inputDto.StartDate.Value);
            if (inputDto.EndDate.HasValue)
                documentQuery = documentQuery.Where(x => x.CreatedAt < inputDto.EndDate.Value);
            var userId = _dbContext.Customer.Where(x => x.Reference == userReference).Select(x => x.Id).FirstOrDefault();
            
            var querableDocument = _dbContext.Document.Where(x => x.Customer.Reference == userReference).AsQueryable();

            var customDocument = querableDocument
                    .Select(d => new
                    {
                        d.Id,
                        d.DocumentDefinitionId,
                        d.Status,
                        d.DocumentContentId,
                        d.CreatedAt
                    }).ToList();

            var querableDocumentIds = customDocument.Select(d => d.DocumentDefinitionId).ToList();
            var quearableContract = contractQuery.IgnoreAutoIncludes().IgnoreQueryFilters()
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
                .Select(cd => new
                {
                    cd.Id,
                    cd.Code,
                    cd.Titles,
                    cd.Status,
                    cd.IsDeleted,
                    customerContractDocumentGroups = cd.ContractDocumentGroupDetails
                        .Select(cdgd => new
                        {
                            cd.Id,
                            cdgd.DocumentGroupId,
                            cdgd.DocumentGroup.Code,
                            cdgd.DocumentGroup.Titles,
                            cdgd.AtLeastRequiredDocument,
                            cdgd.Required,
                            customerContractGroupDocuments = cdgd.DocumentGroup.DocumentGroupDetails.Select(dgd => new
                            {
                                dgd.DocumentDefinitionId,
                                dgd.DocumentDefinition.Titles,
                                dgd.DocumentDefinition.Code,
                                cdgd.Required,
                                Render = dgd.DocumentDefinition.DocumentOnlineSign != null,
                                dgd.DocumentDefinition.Semver,
                                dgd.DocumentDefinition.DocumentOnlineSign,
                                dgd.IsDeleted
                                })
                        }),
                    CustomerContractDocuments = cd.ContractDocumentDetails
                        .Select(cdd => new
                        {
                            cdd.DocumentDefinitionId,
                            cdd.DocumentDefinition.Titles,
                            cdd.DocumentDefinition.Code,
                            cdd.Required,
                            Render = cdd.DocumentDefinition.DocumentOnlineSign != null,
                            cdd.DocumentDefinition.Semver,
                            cdd.DocumentDefinition.DocumentOnlineSign,
                            cdd.IsDeleted

                        }),
                })
                .ToList();
            List<CustomerContractDto> customerContractDtos = new List<CustomerContractDto>();
            List<Guid> allContractDocumentIds = new List<Guid>();

            foreach (var contract in quearableContract)
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
                        var findDocumentVersion = contract.CustomerContractDocuments.Where(x => x.Code == contractDocument.Code).Select(x=>x.Semver).ToArray();
                        var longestVersion = Versioning.FindLargestVersion(findDocumentVersion);
                        var document = customDocument.Where(d => d.DocumentDefinitionId == contractDocument.DocumentDefinitionId).FirstOrDefault();
                        if(document == null && contractDocument.IsDeleted)
                            continue;
                        CustomerContractDocumentDto customerContractDocumentDto = new CustomerContractDocumentDto();
                        customerContractDocumentDto.Id = contractDocument.DocumentDefinitionId;
                        customerContractDocumentDto.Title = contractDocument.Titles.L(inputDto.GetLanguageCode());
                        customerContractDocumentDto.Code = contractDocument.Code;
                        if(document?.Status == ApprovalStatus.Approved && contractDocument.Semver != longestVersion)
                            customerContractDocumentDto.DocumentStatus = AppConsts.Expired;
                        else if(document?.Status == ApprovalStatus.Approved )
                            customerContractDocumentDto.DocumentStatus = AppConsts.Valid;
                        else
                            customerContractDocumentDto.DocumentStatus = AppConsts.InProgress;
                        customerContractDocumentDto.Required = contractDocument.Required;
                        customerContractDocumentDto.Render = contractDocument.Render;
                        customerContractDocumentDto.Version = contractDocument.Semver;
                        customerContractDocumentDto.MinioUrl = document?.DocumentContentId == null ? null : $"{_baseUrl}{_downloadEndpoint}?ObjectId={document.DocumentContentId}";
                        customerContractDocumentDto.ApprovalDate = document?.CreatedAt == null ? DateTime.MinValue : document.CreatedAt;
                        customerContractDocumentDto.OnlineSign = new OnlineSignDto
                        {
                            AllovedClients = contractDocument.DocumentOnlineSign.DocumentAllowedClientDetails.Select(x => x.DocumentAllowedClients.Code).ToList(),
                            ScaRequired = contractDocument.DocumentOnlineSign.Required,
                            DocumentModelTemplate = contractDocument.DocumentOnlineSign.Templates.Select(x => new DocumentTemplateDto
                            {
                                Name = x.Code,
                                MinVersion = x.Version
                            }).ToList()
                        };
                        allContractDocumentIds.Add(contractDocument.DocumentDefinitionId);
                        customerContractDocumentDtos.Add(customerContractDocumentDto);
                    }
                    customerContractDto.CustomerContractDocuments = customerContractDocumentDtos;
                }
                if(contract.customerContractDocumentGroups != null)
                {
                    List<CustomerContractDocumentGroupDto> customerContractDocumentGroupDtos = new List<CustomerContractDocumentGroupDto>();
                    foreach (var contractDocumentGroup in contract.customerContractDocumentGroups)
                    {
                        CustomerContractDocumentGroupDto customerContractDocumentGroupDto = new CustomerContractDocumentGroupDto();
                        List<CustomerContractDocumentDto> customerContractDocumentDtos = new List<CustomerContractDocumentDto>();
                        customerContractDocumentGroupDto.Id = contractDocumentGroup.DocumentGroupId;
                        customerContractDocumentGroupDto.Code = contractDocumentGroup.Code;
                        customerContractDocumentGroupDto.CustomerContractGroupDocuments = new List<CustomerContractDocumentDto>();
                        foreach (var contractDocumentGroupDocument in contractDocumentGroup.customerContractGroupDocuments)
                        {
                            var findDocumentVersion = contractDocumentGroup.customerContractGroupDocuments.Where(x => x.Code == contractDocumentGroupDocument.Code).Select(x=>x.Semver).ToArray();
                            var longestVersion = Versioning.FindLargestVersion(findDocumentVersion);
                            var document = customDocument.Where(d => d.DocumentDefinitionId == contractDocumentGroupDocument.DocumentDefinitionId).FirstOrDefault();
                            CustomerContractDocumentDto customerContractDocumentDto = new CustomerContractDocumentDto();
                            customerContractDocumentDto.Id = contractDocumentGroupDocument.DocumentDefinitionId;
                            customerContractDocumentDto.Title = contractDocumentGroupDocument.Titles.L(inputDto.GetLanguageCode());
                            customerContractDocumentDto.Code = contractDocumentGroupDocument.Code;
                            if(document?.Status == ApprovalStatus.Approved && contractDocumentGroupDocument.Semver != longestVersion)
                                customerContractDocumentDto.DocumentStatus = AppConsts.Expired;
                            else if(document?.Status == ApprovalStatus.Approved )
                                customerContractDocumentDto.DocumentStatus = AppConsts.Valid;
                            else
                                customerContractDocumentDto.DocumentStatus = AppConsts.InProgress;
                            customerContractDocumentDto.Render = contractDocumentGroupDocument.Render;
                            customerContractDocumentDto.Version = contractDocumentGroupDocument.Semver;
                            customerContractDocumentDto.MinioUrl = document?.DocumentContentId == null ? null : $"{_baseUrl}{_downloadEndpoint}?ObjectId={document.DocumentContentId}";
                            customerContractDocumentDto.ApprovalDate = document?.CreatedAt == null ? DateTime.MinValue : document.CreatedAt;
                            customerContractDocumentDto.OnlineSign = new OnlineSignDto
                            {
                                AllovedClients = contractDocumentGroupDocument.DocumentOnlineSign.DocumentAllowedClientDetails.Select(x => x.DocumentAllowedClients.Code).ToList(),
                                ScaRequired = contractDocumentGroupDocument.DocumentOnlineSign.Required,
                                DocumentModelTemplate = contractDocumentGroupDocument.DocumentOnlineSign.Templates.Select(x => new DocumentTemplateDto
                                {
                                    Name = x.Code,
                                    MinVersion = x.Version
                                }).ToList()
                            };
                            allContractDocumentIds.Add(contractDocumentGroupDocument.DocumentDefinitionId);
                            customerContractDocumentDtos.Add(customerContractDocumentDto);
                        }
                        customerContractDocumentGroupDto.AtLeastRequiredDocument = contractDocumentGroup.AtLeastRequiredDocument;
                        customerContractDocumentGroupDto.Required = contractDocumentGroup.Required;
                        //customerContractDocumentDtos daki Tüm dökümanlar Valid mi kontrolü
                        var allValid = customerContractDocumentDtos.TrueForAll(x => x.DocumentStatus == AppConsts.Valid);
                        customerContractDocumentGroupDto.DocumentGroupStatus = allValid?AppConsts.Valid: AppConsts.NotValid;
                        customerContractDocumentGroupDto.Titles = contractDocumentGroup.Titles;
                        customerContractDocumentGroupDto.CustomerContractGroupDocuments = customerContractDocumentDtos;
                        customerContractDocumentGroupDtos.Add(customerContractDocumentGroupDto);
                    }
                    customerContractDto.CustomerContractDocumentGroups = customerContractDocumentGroupDtos;
                }
                customerContractDtos.Add(customerContractDto);
                
            }
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
            customerContractDtos.Add(otherContract);
            if(othersOnly)
            {
                customerContractDtos = new List<CustomerContractDto> { otherContract };
            }
            return GenericResult<List<CustomerContractDto>>.Success(customerContractDtos);
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
            public ApprovalStatus Status { get; set; }
            public string DocumentContentId { get; set; }
        }
    }
}

