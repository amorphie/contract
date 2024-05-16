using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.core.Enum;
using amorphie.contract.infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Response;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Entity.Contract;
using Serilog;

namespace amorphie.contract.application.Contract
{
    public interface IContractAppService
    {

        Task<GenericResult<ContractInstanceDto>> Instance(ContractInstanceInputDto req, CancellationToken cts);
        Task<GenericResult<bool>> InstanceState(ContractInstanceStateInputDto req, CancellationToken cts);
        Task<GenericResult<bool>> GetExist(ContractGetExistInputDto req, CancellationToken cts);

    }
    public class ContractAppService : IContractAppService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IUserSignedContractAppService _userSignedContractAppService;
        private readonly ILogger _logger;

        public ContractAppService(ProjectDbContext dbContext, IUserSignedContractAppService userSignedContractAppService, ILogger logger)
        {
            _dbContext = dbContext;
            _userSignedContractAppService = userSignedContractAppService;
            _logger = logger;
        }


        public async Task<GenericResult<bool>> GetExist(ContractGetExistInputDto req, CancellationToken cts)
        {
            var contractDefinition = await _dbContext.ContractDefinition
                .AnyAsync(x => x.Code == req.Code && x.BankEntity == req.EBankEntity, cts);
            return GenericResult<bool>.Success(contractDefinition);
        }

        public async Task<GenericResult<ContractInstanceDto>> Instance(ContractInstanceInputDto req, CancellationToken cts)
        {

            var contractInstaceResponseDto = await GetContractInstance(req.ContractName, req.Reference, req.LangCode, req.EBankEntity.Value, cts);

            ApprovalStatus contractStatus = SetAndGetContractDocumentStatus(contractInstaceResponseDto.Data.DocumentList, contractInstaceResponseDto.Data.DocumentGroupList);

            Guid contractInstanceId = req.ContractInstanceId;
            await SaveUserSignedContract(contractInstanceId, req, contractInstaceResponseDto.Data.DocumentList, contractInstaceResponseDto.Data.DocumentGroupList, contractStatus);

            var unSignedDocuments = contractInstaceResponseDto.Data.DocumentList.Where(k => !k.IsSigned).ToList();
            var unSignedDocumentGroups = contractInstaceResponseDto.Data.DocumentGroupList.Where(k => k.Status != ApprovalStatus.Approved.ToString()).ToList();

            var contractInstanceDto = new ContractInstanceDto()
            {
                ContractCode = req.ContractName,
                ContractInstanceId = contractInstanceId,
                DocumentList = unSignedDocuments,
                Status = contractStatus.ToString(),
                DocumentGroupList = unSignedDocumentGroups
            };

            return GenericResult<ContractInstanceDto>.Success(contractInstanceDto);
        }

        #region Private Methods
        private async Task<GenericResult<GetContractInstanceResponseDto>> GetContractInstance(string contractCode, string userReference, string langCode, EBankEntity eBankEntity, CancellationToken cancellationToken)
        {
            var contractDefinition = await _dbContext.ContractDefinition.AsNoTracking().FirstOrDefaultAsync(x => x.Code == contractCode && x.BankEntity == eBankEntity, cancellationToken);

            if (contractDefinition is null)
            {
                throw new ArgumentNullException(nameof(contractCode), $"Contract not found : {contractCode}");
            }

            var _allDocumentCodesOfContract = contractDefinition.ContractDocumentDetails
                .Select(x => x.DocumentDefinition.Code)
                    .Concat(contractDefinition.ContractDocumentGroupDetails
                        .SelectMany(x => x.DocumentGroup.DocumentGroupDetails)
                        .Select(cd => cd.DocumentDefinition.Code))
                .ToList();

            var documents = await (from df in _dbContext.DocumentDefinition
                                   join d in _dbContext.Document.Where(x => x.Customer.Reference == userReference) on df.Id equals d.DocumentDefinitionId into userDocuments
                                   from userDoc in userDocuments.DefaultIfEmpty()
                                   where _allDocumentCodesOfContract.Contains(df.Code) && df.IsActive
                                   select new DocumentCustomerInfoDto
                                   {
                                       DocumentDefinitionId = df.Id,
                                       DocumentCode = df.Code,
                                       SemVer = df.Semver,
                                       IsSigned = userDoc.Customer != null && userDoc.Status == ApprovalStatus.Approved,
                                       DocumentInstanceId = userDoc != null ? userDoc.Id : (Guid?)null,
                                       DocumentOnlineSign = new DocumentOnlineSignDto
                                       {
                                           Templates = df.DocumentOnlineSign.Templates.Select(a => new DocumentTemplateDetailsDto
                                           {
                                               Code = a.Code,
                                               LanguageCode = a.LanguageCode,
                                               Version = a.Version
                                           }).ToList()
                                       },
                                       Titles = df.Titles
                                   })
                                   .AsNoTracking()
                                   .ToListAsync();


            List<DocumentInstanceDto> documentInstanceDtos = new();
            foreach (var contractDoc in contractDefinition.ContractDocumentDetails.OrderBy(k => k.Order))
            {
                var documentInstanceDto = MapToDocumentInstanceDto(documents, contractDoc, langCode);
                documentInstanceDtos.Add(documentInstanceDto);
            }

            List<DocumentGroupInstanceDto> docGroupInstanceDtos = new();
            foreach (var contractDocGroupDetail in contractDefinition.ContractDocumentGroupDetails)
            {
                var documentGroupInstanceDto = MapToDocumentGroupInstanceDto(documents, contractDocGroupDetail, langCode);
                docGroupInstanceDtos.Add(documentGroupInstanceDto);
            }

            var response = new GetContractInstanceResponseDto
            {
                DocumentList = documentInstanceDtos,
                DocumentGroupList = docGroupInstanceDtos
            };

            return GenericResult<GetContractInstanceResponseDto>.Success(response);
        }

        private ApprovalStatus SetAndGetContractDocumentStatus(List<DocumentInstanceDto> documentInstanceDtos, List<DocumentGroupInstanceDto> documentGroupInstanceDtos)
        {
            bool unSignedDocument = documentInstanceDtos.Any(k => k.IsRequired && !k.IsSigned);
            foreach (var item in documentGroupInstanceDtos)
            {
                var signedCount = item.DocumentGroupDetailInstance.DocumentInstances.Count(k => k.IsRequired && k.IsSigned);
                if (signedCount >= item.AtLeastRequiredDocument)
                {
                    item.Status = ApprovalStatus.Approved.ToString();
                }
            }

            var unCompletedGroup = documentGroupInstanceDtos.Any(k => k.Status != ApprovalStatus.Approved.ToString() && k.Required);

            if (unSignedDocument || unCompletedGroup)
            {
                return ApprovalStatus.InProgress;
            }
            else
            {
                return ApprovalStatus.Approved;
            }

        }

        private DocumentInstanceDto MapToDocumentInstanceDto(IEnumerable<DocumentCustomerInfoDto> documents, ContractDocumentDetail contractDoc, string langCode)
        {
            var documentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code).ToArray();

            var findDocumentLastVersion = Versioning.FindHighestVersion(documentsVersionByCode.Select(k => k.SemVer).ToArray());

            var documentLastVersion = documentsVersionByCode.First(x => x.SemVer == findDocumentLastVersion);

            var signedDocumentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && k.IsSigned).Select(k => k.SemVer).ToArray();
            var findUserSignedLastVersion = Versioning.FindHighestVersion(signedDocumentsVersionByCode);

            // Checking contract document min version...
            var customerDocument = documents.FirstOrDefault(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && Versioning.CompareVersion(findUserSignedLastVersion, contractDoc.DocumentDefinition.Semver) && k.IsSigned);
            var template = documentLastVersion.DocumentOnlineSign?.Templates.FirstOrDefault(x => x.LanguageCode == langCode);


            var documentInstance = new DocumentInstanceDto
            {
                Code = contractDoc.DocumentDefinition.Code,
                UseExisting = contractDoc.UseExisting.ToString(),
                IsRequired = contractDoc.Required,
                Status = ApprovalStatus.InProgress.ToString(),
                MinVersion = contractDoc.DocumentDefinition.Semver,
                LastVersion = findDocumentLastVersion,
                Name = documentLastVersion.Titles.L(langCode),
                DocumentDetail = new DocumentInstanceDetailDto
                {
                    OnlineSign = new DocumentInstanceOnlineSignDto
                    {
                        TemplateCode = template?.Code,
                        Version = template?.Version
                    }
                }
            };

            if (customerDocument is not null)
            {
                documentInstance.DocumentInstanceId = customerDocument.DocumentInstanceId;
                documentInstance.Status = ApprovalStatus.Approved.ToString();
                documentInstance.Sign();
            }
            else
            {
                if (documents.Any(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && k.IsSigned))
                {
                    documentInstance.Status = ApprovalStatus.HasNewVersion.ToString();
                }
            }

            return documentInstance;
        }

        private DocumentGroupInstanceDto MapToDocumentGroupInstanceDto(IEnumerable<DocumentCustomerInfoDto> documents, ContractDocumentGroupDetail contractDocGroupDetail, string langCode)
        {
            var docGroupInstanceDto = new DocumentGroupInstanceDto
            {
                AtLeastRequiredDocument = contractDocGroupDetail.AtLeastRequiredDocument,
                Required = contractDocGroupDetail.Required,
                Title = contractDocGroupDetail.DocumentGroup.Titles.L(langCode),
                DocumentGroupDetailInstance = new DocumentGroupDetailInstanceDto()
            };

            foreach (var contractDoc in contractDocGroupDetail.DocumentGroup.DocumentGroupDetails.ToList())
            {

                var documentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code).ToArray();
                var findDocumentLastVersion = Versioning.FindHighestVersion(documentsVersionByCode.Select(k => k.SemVer).ToArray());

                var documentLastVersion = documentsVersionByCode.First(x => x.SemVer == findDocumentLastVersion);

                var signedDocumentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && k.IsSigned).Select(k => k.SemVer).ToArray();
                var findUserSignedLastVersion = Versioning.FindHighestVersion(signedDocumentsVersionByCode);

                docGroupInstanceDto.DocumentGroupDetailInstance.Code = contractDoc.DocumentGroup.Code;

                // Checking contract document min version...
                var customerDocument = documents.FirstOrDefault(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && Versioning.CompareVersion(findUserSignedLastVersion, contractDoc.DocumentDefinition.Semver) && k.IsSigned);
                var template = documentLastVersion.DocumentOnlineSign.Templates.FirstOrDefault(x => x.LanguageCode == langCode);


                var documentInstance = new DocumentInstanceDto
                {
                    Code = contractDoc.DocumentDefinition.Code,
                    UseExisting = EUseExisting.AnyValid.ToString(),
                    IsRequired = contractDocGroupDetail.Required,
                    MinVersion = contractDoc.DocumentDefinition.Semver,
                    LastVersion = findDocumentLastVersion,
                    Name = documentLastVersion.Titles.L(langCode),
                    Status = ApprovalStatus.InProgress.ToString(),
                    DocumentDetail = new DocumentInstanceDetailDto
                    {
                        OnlineSign = new DocumentInstanceOnlineSignDto
                        {
                            TemplateCode = template?.Code,
                            Version = template?.Version
                        }
                    },

                };

                if (customerDocument is not null)
                {
                    documentInstance.DocumentInstanceId = customerDocument.DocumentInstanceId;
                    documentInstance.Status = ApprovalStatus.Approved.ToString();
                    documentInstance.Sign();
                }
                else
                {
                    if (documents.Any(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && k.IsSigned))
                    {
                        documentInstance.Status = ApprovalStatus.HasNewVersion.ToString();
                    }
                }

                docGroupInstanceDto.DocumentGroupDetailInstance.DocumentInstances.Add(documentInstance);

            }

            return docGroupInstanceDto;
        }

        private async Task SaveUserSignedContract(Guid contractInstanceId, ContractInstanceInputDto req, List<DocumentInstanceDto> documentInstanceDtos, List<DocumentGroupInstanceDto> docGroupInstanceDtos, ApprovalStatus contractStatus)
        {

            var allDocumentInstanceIds = documentInstanceDtos
            .Where(d => d.IsSigned && d.DocumentInstanceId.HasValue)
                .Select(d => d.DocumentInstanceId.Value)
            .Concat(
                docGroupInstanceDtos
            .Where(k => k.DocumentGroupDetailInstance.DocumentInstances.Any(x => x.IsSigned && x.DocumentInstanceId.HasValue))
                .SelectMany(d => d.DocumentGroupDetailInstance.DocumentInstances)
                .Select(x => x.DocumentInstanceId.Value)).ToList();

            var userSignedInput = new UserSignedContractInputDto
            {
                ContractCode = req.ContractName,
                ContractInstanceId = contractInstanceId,
                DocumentInstanceIds = allDocumentInstanceIds,
                ApprovalStatus = contractStatus
            };
            userSignedInput.SetHeaderParameters(req.Reference);

            var result = await _userSignedContractAppService.UpsertAsync(userSignedInput);
            if (!result.IsSuccess)
            {
                _logger.Error("Failed to upsert userSignedContract. {Message} - {ContractCode} {ContractInstanceId}", result.ErrorMessage, req.ContractName, req.ContractInstanceId);
            }

        }

        #endregion

        public async Task<GenericResult<bool>> InstanceState(ContractInstanceStateInputDto req, CancellationToken cts)
        {
            var userSignedInput = new IsUserApprovedContractInputDto(req.ContractName, req.Reference);

            var isUserApprovedContact = await _userSignedContractAppService.IsUserApprovedContract(userSignedInput);

            if (!isUserApprovedContact.IsSuccess)
            {
                return GenericResult<bool>.Success(false);
            }

            if (isUserApprovedContact.Data.HasValue)
            {
                return GenericResult<bool>.Success(isUserApprovedContact.Data.Value);
            }
            else
            {
                var contractInstaceResponseDto = await GetContractInstance(req.ContractName, req.Reference, req.LangCode, req.EBankEntity.Value, cts);

                ApprovalStatus contractStatus = SetAndGetContractDocumentStatus(contractInstaceResponseDto.Data.DocumentList, contractInstaceResponseDto.Data.DocumentGroupList);

                return GenericResult<bool>.Success(contractStatus == ApprovalStatus.Approved ? true : false);
            }
        }
    }




}
