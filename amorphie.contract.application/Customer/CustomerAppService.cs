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
                        CreatedAt=d.CreatedAt,
                        Version=d.DocumentDefinition.Semver
                    }).ToList();


            // Bir userin kontraktlarının silinmiş döküman ve kontraktları da dahil olmak üzere hepsini getirir.
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

        //Kontrakt özelindeki Mapleme işlemlerini yapar. Döküman ve Döküman gruplarını Mapleyen metodlar ayrılmıştır.
        //Döküman ve Grupların statusune göre kontrakt statusu belrlenir
        //Bir kontrakta bağlı olmayan metodlar da bu kısımdadır.
        private async Task<List<CustomerContractDto>> MapToCustomerContract(
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
                    customerContractDto.CustomerContractDocuments = CustomerContractDocumentMapper(contract.CustomerContractDocuments, customerDocument, inputDto, allContractDocumentIds);
                if(contract.CustomerContractDocumentGroups != null)
                    customerContractDto.CustomerContractDocumentGroups = CustomerContractDocumentGroupMapper(contract.CustomerContractDocumentGroups, customerDocument, inputDto, allContractDocumentIds);

                var allValid = customerContractDtos.Find(
                    x => x.CustomerContractDocuments.All(y => y.DocumentStatus == ApprovalStatus.Approved.ToString() || y.DocumentStatus == ApprovalStatus.HasNewVersion.ToString())
                    && 
                    x.CustomerContractDocumentGroups.All(y => y.DocumentGroupStatus == ApprovalStatus.Approved.ToString())
                );

                if(allValid != null)
                    customerContractDto.contractStatus = ApprovalStatus.Approved.ToString();
                else if(contract.IsDeleted != null && contract.IsDeleted.Value)
                    customerContractDto.contractStatus = ApprovalStatus.Canceled.ToString();
                else
                    customerContractDto.contractStatus = ApprovalStatus.InProgress.ToString();
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

        // Bu metod Bir müşteriye ait contractın dökümanlarını Versiyonlarına göre, silindi silinmedi durumlarına göre check eder ve statusleri doldurur.
        // Eğer döküman hiçbir şekilde onaylanmamışsa dökümana dair minioUrl, approvalDate değerlerini null getirir.
        private List<CustomerContractDocumentDto> CustomerContractDocumentMapper(
            IEnumerable<CustomerContractDocumentDto> customerContractDocuments, 
            List<CustomerDocumentDto> customerDocument, 
            GetCustomerDocumentsByContractInputDto inputDto, 
            List<Guid> allContractDocumentIds)
        {
            List<CustomerContractDocumentDto> customerContractDocumentDtos = new List<CustomerContractDocumentDto>();
            var findCustomerDocumentVersion = customerContractDocuments.Select(x=>x.Version).ToArray();
            foreach (var contractDocument in customerContractDocuments)
            {
                CustomerContractDocumentDto customerContractDocumentDto = new CustomerContractDocumentDto();
                var document = customerDocument.Find(d => d.DocumentDefinitionId == contractDocument.Id);
                if(document == null && contractDocument.IsDeleted)
                    continue;
                if(document == null)
                    customerContractDocumentDto.DocumentStatus = ApprovalStatus.InProgress.ToString();
                else
                {
                    var longestVersion = Versioning.FindHighestVersion(findCustomerDocumentVersion);
                    var compareVersion = Versioning.CompareVersion(longestVersion, document.Version);
                    if(compareVersion && longestVersion != document.Version)
                        customerContractDocumentDto.DocumentStatus = ApprovalStatus.HasNewVersion.ToString();
                    else
                        customerContractDocumentDto.DocumentStatus = ApprovalStatus.Approved.ToString();
                }
                customerContractDocumentDto.Id = contractDocument.Id;
                customerContractDocumentDto.Title = contractDocument.Titles.L(inputDto.GetLanguageCode());
                customerContractDocumentDto.Code = contractDocument.Code;
                customerContractDocumentDto.Required = contractDocument.Required;
                customerContractDocumentDto.Render = contractDocument.Render;
                customerContractDocumentDto.Version = contractDocument.Version;
                customerContractDocumentDto.MinioUrl = document?.DocumentContentId == null ? null : $"{_baseUrl}{_downloadEndpoint}?ObjectId={document.DocumentContentId}";
                customerContractDocumentDto.ApprovalDate = document?.CreatedAt == null ? null : document.CreatedAt;
                customerContractDocumentDto.OnlineSign = contractDocument.OnlineSign;
                allContractDocumentIds.Add(contractDocument.Id);
                customerContractDocumentDtos.Add(customerContractDocumentDto);
            }
            return customerContractDocumentDtos;
        }
        // CustomerContractDocumentMapper ta yapılan işi Group özelinde yapar
        private List<CustomerContractDocumentGroupDto> CustomerContractDocumentGroupMapper(
            IEnumerable<CustomerContractDocumentGroupDto> customerContractDocumentGroup,
            List<CustomerDocumentDto> customerDocument,
            GetCustomerDocumentsByContractInputDto inputDto,
            List<Guid> allContractDocumentIds)
        {
            List<CustomerContractDocumentGroupDto> customerContractDocumentGroupDtos = new List<CustomerContractDocumentGroupDto>();
            foreach (var contractDocumentGroup in customerContractDocumentGroup)
            {
                CustomerContractDocumentGroupDto customerContractDocumentGroupDto = new CustomerContractDocumentGroupDto();
                List<CustomerContractDocumentDto> customerContractDocumentDtos = new List<CustomerContractDocumentDto>();
                customerContractDocumentGroupDto.Id = contractDocumentGroup.Id;
                customerContractDocumentGroupDto.Code = contractDocumentGroup.Code;
                customerContractDocumentGroupDto.CustomerContractGroupDocuments = new List<CustomerContractDocumentDto>();
                var findCustomerDocumentVersion = contractDocumentGroup.CustomerContractGroupDocuments.Select(x=>x.Version).ToArray();
                foreach (var contractDocumentGroupDocument in contractDocumentGroup.CustomerContractGroupDocuments)
                {
                    CustomerContractDocumentDto customerContractDocumentDto = new CustomerContractDocumentDto();
                    var document = customerDocument.Find(d => d.DocumentDefinitionId == contractDocumentGroupDocument.Id);
                    if(document == null && contractDocumentGroupDocument.IsDeleted)
                        continue;
                    if(document == null)
                        customerContractDocumentDto.DocumentStatus = ApprovalStatus.InProgress.ToString();
                    else
                    {
                        var longestVersion = Versioning.FindHighestVersion(findCustomerDocumentVersion);
                        var compareVersion = Versioning.CompareVersion(document.Version, longestVersion);
                        if(compareVersion && longestVersion != document.Version)
                            customerContractDocumentDto.DocumentStatus = ApprovalStatus.HasNewVersion.ToString();
                        else
                            customerContractDocumentDto.DocumentStatus = ApprovalStatus.Approved.ToString();
                    }
                    customerContractDocumentDto.Id = contractDocumentGroupDocument.Id;
                    customerContractDocumentDto.Title = contractDocumentGroupDocument.Titles.L(inputDto.GetLanguageCode());
                    customerContractDocumentDto.Code = contractDocumentGroupDocument.Code;
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
                var allValid = customerContractDocumentDtos.TrueForAll(x => x.DocumentStatus == ApprovalStatus.Approved.ToString() || x.DocumentStatus == ApprovalStatus.HasNewVersion.ToString());
                customerContractDocumentGroupDto.DocumentGroupStatus = allValid?ApprovalStatus.Approved.ToString(): ApprovalStatus.InProgress.ToString();
                customerContractDocumentGroupDto.Titles = contractDocumentGroup.Titles;
                customerContractDocumentGroupDto.CustomerContractGroupDocuments = customerContractDocumentDtos;
                customerContractDocumentGroupDtos.Add(customerContractDocumentGroupDto);
            }
            return customerContractDocumentGroupDtos;
        }
        private async Task<CustomerContractDto> GetOtherDocuments(IQueryable<Document> documentQuery, List<Guid> allContractDocumentIds,GetCustomerDocumentsByContractInputDto inputDto)
        {
            var otherDocuments = await documentQuery.Where(d=>!allContractDocumentIds.Contains(d.DocumentDefinitionId))
                    .Select(d=> new CustomerContractDocumentDto
                        {
                            Id = d.DocumentDefinitionId,
                            Title = d.DocumentDefinition.Titles.L(inputDto.GetLanguageCode()),
                            Code = d.DocumentDefinition.Code,
                            DocumentStatus = d.Status == ApprovalStatus.Approved ? ApprovalStatus.Approved.ToString() : ApprovalStatus.InProgress.ToString(),
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
                        }).ToListAsync();
            var otherContract = new CustomerContractDto
            {
                Code = "//OtherDocuments",
                Titles = new Dictionary<string, string> { { "tr", "Diğer Belgeler" }, { "en", "Other Documents" } },
                Title = inputDto.GetLanguageCode() == "tr-TR" ? "Diğer" : "Other",
                CustomerContractDocuments = otherDocuments
            };
            return otherContract;
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

