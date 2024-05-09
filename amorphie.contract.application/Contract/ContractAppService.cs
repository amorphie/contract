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
        Task<GenericResult<bool>> InstanceState(ContractInstanceInputDto req, CancellationToken cts);

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
            var contractDefinition = await _dbContext.ContractDefinition.AsNoTracking().FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);

            if (contractDefinition == null)
            {
                throw new ArgumentNullException("Contract not found.");
            }

            var allDocumentIdsOfContract = contractDefinition.ContractDocumentDetails
                .Select(x => new { Id = x.DocumentDefinitionId, Code = x.DocumentDefinition.Code })
                    .Concat(contractDefinition.ContractDocumentGroupDetails
                        .SelectMany(x => x.DocumentGroup.DocumentGroupDetails)
                        .Select(cd => new { Id = cd.DocumentDefinitionId, Code = cd.DocumentDefinition.Code }))
                .ToList();


            var allDocumentCodeList = allDocumentIdsOfContract.Select(a => a.Code).ToList();

            var documents = await (from df in _dbContext.DocumentDefinition
                                   join d in _dbContext.Document.Where(x => x.Customer.Reference == req.Reference) on df.Id equals d.DocumentDefinitionId into userDocuments
                                   from userDoc in userDocuments.DefaultIfEmpty()
                                   where allDocumentCodeList.Contains(df.Code)
                                   select new DocumentCustomerInfoDto
                                   {
                                       DocumentDefinitionId = df.Id,
                                       DocumentCode = df.Code,
                                       SemVer = df.Semver,
                                       IsSigned = userDoc.Customer != null,
                                       DocumentInstanceId = userDoc != null ? userDoc.Id : (Guid?)null,
                                   })
                                   .AsNoTracking()
                                   .ToListAsync();


            List<DocumentInstanceDto> documentInstanceDtos = new();

            foreach (var contractDoc in contractDefinition.ContractDocumentDetails)
            {
                var documentInstanceDto = MapToDocumentInstanceDto(documents, contractDoc, req.LangCode);
                documentInstanceDtos.Add(documentInstanceDto);
            }


            var docGroupInstanceDtos = new List<DocumentGroupInstanceDto>();
            foreach (var contractDocGroupDetail in contractDefinition.ContractDocumentGroupDetails)
            {
                var documentGroupInstanceDto = MapToDocumentGroupInstanceDto(documents, contractDocGroupDetail, req.LangCode);
                docGroupInstanceDtos.Add(documentGroupInstanceDto);
            }

            Guid contractInstanceId = req.ContractInstanceId;

            await SaveUserSignedContract(contractInstanceId, req, documentInstanceDtos, docGroupInstanceDtos);

            string contractStatus = SetAndGetContractDocumentStatus(documentInstanceDtos, docGroupInstanceDtos);
            
            var unSignedDocuments = documentInstanceDtos.Where(k => !k.IsSigned).ToList();

            var contractInstanceDto = new ContractInstanceDto()
            {
                ContractCode = contractDefinition.Code,
                ContractInstanceId = contractInstanceId,
                DocumentList = unSignedDocuments,
                Status = contractStatus,
                DocumentGroupList = docGroupInstanceDtos.Where(k => k.Status != ApprovalStatus.Approved.ToString()).ToList()
            };

            return GenericResult<ContractInstanceDto>.Success(contractInstanceDto);
        }

        private string SetAndGetContractDocumentStatus(List<DocumentInstanceDto> documentInstanceDtos, List<DocumentGroupInstanceDto> documentGroupInstanceDtos)
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
                return ApprovalStatus.InProgress.ToString();
            }
            else
            {
                return ApprovalStatus.Approved.ToString();
            }

        }

        private DocumentInstanceDto MapToDocumentInstanceDto(IEnumerable<DocumentCustomerInfoDto> documents, ContractDocumentDetail contractDoc, string langCode)
        {
            var documentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code).Select(k => k.SemVer).ToArray();
            var findDocumentLastVersion = Versioning.FindHighestVersion(documentsVersionByCode);

            var signedDocumentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && k.IsSigned).Select(k => k.SemVer).ToArray();
            var findUserSignedLastVersion = Versioning.FindHighestVersion(signedDocumentsVersionByCode);

            // Checking contract document min version...
            var customerDocument = documents.FirstOrDefault(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && Versioning.CompareVersion(findUserSignedLastVersion, contractDoc.DocumentDefinition.Semver) && k.IsSigned);
            var template = contractDoc.DocumentDefinition?.DocumentOnlineSign?.Templates.FirstOrDefault(x => x.LanguageCode == langCode);

            var documentInstance = new DocumentInstanceDto
            {
                Code = contractDoc.DocumentDefinition.Code,
                UseExisting = contractDoc.UseExisting.ToString(),
                IsRequired = contractDoc.Required,
                Status = ApprovalStatus.InProgress.ToString(),
                MinVersion = contractDoc.DocumentDefinition.Semver,
                LastVersion = findDocumentLastVersion,
                Name = contractDoc.DocumentDefinition.Titles.L(langCode),
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
                Title = contractDocGroupDetail.DocumentGroup.Titles.L(langCode)
            };

            foreach (var contractDoc in contractDocGroupDetail.DocumentGroup.DocumentGroupDetails.ToList())
            {

                var documentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code).Select(k => k.SemVer).ToArray();
                var findDocumentLastVersion = Versioning.FindHighestVersion(documentsVersionByCode);

                var signedDocumentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && k.IsSigned).Select(k => k.SemVer).ToArray();
                var findUserSignedLastVersion = Versioning.FindHighestVersion(signedDocumentsVersionByCode);

                docGroupInstanceDto.DocumentGroupDetailInstance = new DocumentGroupDetailInstanceDto
                {
                    Code = contractDoc.DocumentGroup.Code,
                };

                // Checking contract document min version...
                var customerDocument = documents.FirstOrDefault(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && Versioning.CompareVersion(findUserSignedLastVersion, contractDoc.DocumentDefinition.Semver) && k.IsSigned);
                var template = contractDoc.DocumentDefinition.DocumentOnlineSign.Templates.FirstOrDefault(x => x.LanguageCode == langCode);


                var documentInstance = new DocumentInstanceDto
                {
                    Code = contractDoc.DocumentDefinition.Code,
                    UseExisting = EUseExisting.AnyValid.ToString(),
                    IsRequired = contractDocGroupDetail.Required,
                    MinVersion = contractDoc.DocumentDefinition.Semver,
                    LastVersion = findDocumentLastVersion,
                    Name = contractDoc.DocumentDefinition.Titles.L(langCode),
                    Status = ApprovalStatus.InProgress.ToString(),
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

                docGroupInstanceDto.DocumentGroupDetailInstance.DocumentInstances.Add(documentInstance);

            }

            return docGroupInstanceDto;
        }

        private async Task SaveUserSignedContract(Guid contractInstanceId, ContractInstanceInputDto req, List<DocumentInstanceDto> documentInstanceDtos, List<DocumentGroupInstanceDto> docGroupInstanceDtos)
        {

            var allDocumentInstanceIds = documentInstanceDtos.Where(d => d.IsSigned && d.DocumentInstanceId.HasValue)
                .Select(d => d.DocumentInstanceId.Value)
            .Concat(docGroupInstanceDtos
                .Where(k => k.DocumentGroupDetailInstance.DocumentInstances.Any(x => x.IsSigned && x.DocumentInstanceId.HasValue))
                    .SelectMany(d => d.DocumentGroupDetailInstance.DocumentInstances)
                    .Select(x => x.DocumentInstanceId.Value)).ToList();

            var userSignedInput = new UserSignedContractInputDto
            {
                ContractCode = req.ContractName,
                ContractInstanceId = contractInstanceId,
                DocumentInstanceIds = allDocumentInstanceIds
            };
            userSignedInput.SetHeaderParameters(req.Reference);

            var result = await _userSignedContractAppService.UpsertAsync(userSignedInput);
            if (!result.IsSuccess)
            {
                _logger.Error("Failed to upsert userSignedContract. {Message} - {ContractCode} {ContractInstanceId}", result.ErrorMessage, req.ContractName, req.ContractInstanceId);
            }

        }

        public async Task<GenericResult<bool>> InstanceState(ContractInstanceInputDto req, CancellationToken cts)
        {
            //TODO: Daha sonra eklenecek && x.BankEntity == req.EBankEntity
            var contractDefinition = await _dbContext.ContractDefinition.FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);
            // var ss = await _dbContext.ContractDefinition.Include(x=>x.ContractDocumentDetails).FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);
            if (contractDefinition == null)
            {
                return GenericResult<bool>.Success(false);

            }

            var documentList = contractDefinition.ContractDocumentDetails
                .Select(x => x.DocumentDefinitionId)
                .ToList();

            var documentGroupList = contractDefinition.ContractDocumentGroupDetails
                .SelectMany(x => x.DocumentGroup.DocumentGroupDetails)
                .Select(a => a.DocumentDefinitionId)
                .ToList();

            var customerDocument = await _dbContext.Document
                .Where(x => x.Customer.Reference == req.Reference && documentList.Contains(x.DocumentDefinitionId))
                .Select(x => x.DocumentDefinitionId)
                .ToListAsync(cts);

            var customerDocumentGroup = await _dbContext.Document
                .Where(x => x.Customer.Reference == req.Reference && documentGroupList.Contains(x.DocumentDefinitionId))
                .Select(x => x.DocumentDefinitionId)
                .ToListAsync(cts);

            var listDocument = contractDefinition.ContractDocumentDetails
                .Where(d => !customerDocument.Contains(d.DocumentDefinitionId));

            var listDocumentGroup = contractDefinition.ContractDocumentGroupDetails.ToList();

            var contractDocumentDetails = ObjectMapperApp.Mapper.Map<List<ContractDocumentDetailDto>>(listDocument, opt => opt.Items[Lang.LangCode] = req.LangCode);
            var contractDocumentGroupDetails = ObjectMapperApp.Mapper.Map<List<ContractDocumentGroupDetailDto>>(listDocumentGroup, opt => opt.Items[Lang.LangCode] = req.LangCode);

            var contractInstanceDto = new ContractInstanceDto()
            {
                ContractCode = contractDefinition.Code,
                Status = ApprovalStatus.InProgress.ToString(),
                DocumentList = ObjectMapperApp.Mapper.Map<List<DocumentInstanceDto>>(contractDocumentDetails, opt => opt.Items[Lang.LangCode] = req.LangCode),
                DocumentGroupList = ObjectMapperApp.Mapper.Map<List<DocumentGroupInstanceDto>>(contractDocumentGroupDetails, opt => opt.Items[Lang.LangCode] = req.LangCode)
            };
            if (contractInstanceDto.DocumentList.Count == 0)
                return GenericResult<bool>.Success(true);
            return GenericResult<bool>.Success(false);
        }
    }




}
