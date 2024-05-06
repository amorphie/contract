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
using amorphie.contract.application.Contract.Dto;
using System.Reflection.Metadata;

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
                    });

            var querableDocumentIds = customDocument.Select(d => d.DocumentDefinitionId).ToList();
            var quearableContract = _dbContext.ContractDefinition.IgnoreAutoIncludes().IgnoreQueryFilters()
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
            List<CustomerContractDto> customerContractDtos1 = new List<CustomerContractDto>();
            List<Guid> allContractDocumentIds1 = new List<Guid>();

            foreach (var contract in quearableContract)
            {
                CustomerContractDto customerContractDto1 = new CustomerContractDto();
                customerContractDto1.Id = contract.Id;
                customerContractDto1.Code = contract.Code;
                customerContractDto1.Titles = contract.Titles;
                customerContractDto1.Title = contract.Titles.L(inputDto.GetLanguageCode());

                if(contract.CustomerContractDocuments != null)
                {

                    List<CustomerContractDocumentDto> customerContractDocumentDtos1 = new List<CustomerContractDocumentDto>();
                    foreach (var contractDocument in contract.CustomerContractDocuments)
                    {
                        var findDocumentVersion = contract.CustomerContractDocuments.Where(x => x.Code == contractDocument.Code).Select(x=>x.Semver).ToArray();
                        var longestVersion = Versioning.FindLargestVersion(findDocumentVersion);
                        var document = customDocument.Where(d => d.DocumentDefinitionId == contractDocument.DocumentDefinitionId).FirstOrDefault();
                        if(document == null && contractDocument.IsDeleted)
                            continue;
                        CustomerContractDocumentDto customerContractDocumentDto1 = new CustomerContractDocumentDto();
                        customerContractDocumentDto1.Id = contractDocument.DocumentDefinitionId;
                        customerContractDocumentDto1.Title = contractDocument.Titles.L(inputDto.GetLanguageCode());
                        customerContractDocumentDto1.Code = contractDocument.Code;
                        if(document?.Status == EStatus.Completed && contractDocument.Semver != longestVersion)
                            customerContractDocumentDto1.DocumentStatus = AppConsts.Expired;
                        else if(document?.Status == EStatus.Completed )
                            customerContractDocumentDto1.DocumentStatus = AppConsts.Valid;
                        else
                            customerContractDocumentDto1.DocumentStatus = AppConsts.InProgress;
                        customerContractDocumentDto1.Required = contractDocument.Required;
                        customerContractDocumentDto1.Render = contractDocument.Render;
                        customerContractDocumentDto1.Version = contractDocument.Semver;
                        customerContractDocumentDto1.MinioUrl = document?.DocumentContentId == null ? null : $"{_baseUrl}{_downloadEndpoint}?ObjectId={document.DocumentContentId}";
                        customerContractDocumentDto1.ApprovalDate = document?.CreatedAt == null ? DateTime.MinValue : document.CreatedAt;
                        customerContractDocumentDto1.OnlineSign = new OnlineSignDto
                        {
                            AllovedClients = contractDocument.DocumentOnlineSign.DocumentAllowedClientDetails.Select(x => x.DocumentAllowedClients.Code).ToList(),
                            ScaRequired = contractDocument.DocumentOnlineSign.Required,
                            DocumentModelTemplate = contractDocument.DocumentOnlineSign.Templates.Select(x => new DocumentTemplateDto
                            {
                                Name = x.Code,
                                MinVersion = x.Version
                            }).ToList()
                        };
                        allContractDocumentIds1.Add(contractDocument.DocumentDefinitionId);
                        customerContractDocumentDtos1.Add(customerContractDocumentDto1);
                    }
                    customerContractDto1.CustomerContractDocuments = customerContractDocumentDtos1;
                }
                if(contract.customerContractDocumentGroups != null)
                {
                    List<CustomerContractDocumentGroupDto> customerContractDocumentGroupDtos1 = new List<CustomerContractDocumentGroupDto>();
                    foreach (var contractDocumentGroup in contract.customerContractDocumentGroups)
                    {
                        CustomerContractDocumentGroupDto customerContractDocumentGroupDto1 = new CustomerContractDocumentGroupDto();
                        List<CustomerContractDocumentDto> customerContractDocumentDtos1 = new List<CustomerContractDocumentDto>();
                        customerContractDocumentGroupDto1.Id = contractDocumentGroup.DocumentGroupId;
                        customerContractDocumentGroupDto1.Code = contractDocumentGroup.Code;
                        customerContractDocumentGroupDto1.CustomerContractGroupDocuments = new List<CustomerContractDocumentDto>();
                        foreach (var contractDocumentGroupDocument in contractDocumentGroup.customerContractGroupDocuments)
                        {
                            var findDocumentVersion = contractDocumentGroup.customerContractGroupDocuments.Where(x => x.Code == contractDocumentGroupDocument.Code).Select(x=>x.Semver).ToArray();
                            var longestVersion = Versioning.FindLargestVersion(findDocumentVersion);
                            var document = customDocument.Where(d => d.DocumentDefinitionId == contractDocumentGroupDocument.DocumentDefinitionId).FirstOrDefault();
                            CustomerContractDocumentDto customerContractDocumentDto2 = new CustomerContractDocumentDto();
                            customerContractDocumentDto2.Id = contractDocumentGroupDocument.DocumentDefinitionId;
                            customerContractDocumentDto2.Title = contractDocumentGroupDocument.Titles.L(inputDto.GetLanguageCode());
                            customerContractDocumentDto2.Code = contractDocumentGroupDocument.Code;
                            if(document?.Status == EStatus.Completed && contractDocumentGroupDocument.Semver != longestVersion)
                                customerContractDocumentDto2.DocumentStatus = AppConsts.Expired;
                            else if(document?.Status == EStatus.Completed )
                                customerContractDocumentDto2.DocumentStatus = AppConsts.Valid;
                            else
                                customerContractDocumentDto2.DocumentStatus = AppConsts.InProgress;
                            customerContractDocumentDto2.Render = contractDocumentGroupDocument.Render;
                            customerContractDocumentDto2.Version = contractDocumentGroupDocument.Semver;
                            customerContractDocumentDto2.MinioUrl = document?.DocumentContentId == null ? null : $"{_baseUrl}{_downloadEndpoint}?ObjectId={document.DocumentContentId}";
                            customerContractDocumentDto2.ApprovalDate = document?.CreatedAt == null ? DateTime.MinValue : document.CreatedAt;
                            customerContractDocumentDto2.OnlineSign = new OnlineSignDto
                            {
                                AllovedClients = contractDocumentGroupDocument.DocumentOnlineSign.DocumentAllowedClientDetails.Select(x => x.DocumentAllowedClients.Code).ToList(),
                                ScaRequired = contractDocumentGroupDocument.DocumentOnlineSign.Required,
                                DocumentModelTemplate = contractDocumentGroupDocument.DocumentOnlineSign.Templates.Select(x => new DocumentTemplateDto
                                {
                                    Name = x.Code,
                                    MinVersion = x.Version
                                }).ToList()
                            };
                            allContractDocumentIds1.Add(contractDocumentGroupDocument.DocumentDefinitionId);
                            customerContractDocumentDtos1.Add(customerContractDocumentDto2);
                        }
                        customerContractDocumentGroupDto1.AtLeastRequiredDocument = contractDocumentGroup.AtLeastRequiredDocument;
                        customerContractDocumentGroupDto1.Required = contractDocumentGroup.Required;
                        customerContractDocumentGroupDto1.DocumentGroupStatus = AppConsts.NotValid;
                        customerContractDocumentGroupDto1.Titles = contractDocumentGroup.Titles;
                        customerContractDocumentGroupDto1.CustomerContractGroupDocuments = customerContractDocumentDtos1;
                        customerContractDocumentGroupDtos1.Add(customerContractDocumentGroupDto1);
                    }
                    customerContractDto1.CustomerContractDocumentGroups = customerContractDocumentGroupDtos1;
                }
                customerContractDtos1.Add(customerContractDto1);
                
            }
            var otherDocuments = documentQuery.Where(d=>!allContractDocumentIds1.Contains(d.DocumentDefinitionId))
                    .Select(d=> new CustomerContractDocumentDto
                        {
                            Id = d.DocumentDefinitionId,
                            Title = d.DocumentDefinition.Titles.L(inputDto.GetLanguageCode()),
                            Code = d.DocumentDefinition.Code,
                            DocumentStatus = d.Status == EStatus.Completed ? AppConsts.Valid : AppConsts.InProgress,
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
            customerContractDtos1.Add(otherContract);
            if(othersOnly)
            {
                customerContractDtos1 = new List<CustomerContractDto> { otherContract };
            }
            return GenericResult<List<CustomerContractDto>>.Success(customerContractDtos1);
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

