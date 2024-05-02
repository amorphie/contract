using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.core.Enum;
using amorphie.contract.infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Response;
using amorphie.contract.core.Extensions;

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
        public ContractAppService(ProjectDbContext dbContext, IUserSignedContractAppService userSignedContractAppService)
        {
            _dbContext = dbContext;
            _userSignedContractAppService = userSignedContractAppService;
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
                throw new ArgumentNullException("not contract");
            }
            EStatus contractStatus = EStatus.Completed;

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
                                   select new
                                   {
                                       DocumentDefinitionId = df.Id,
                                       DocumentCode = df.Code,
                                       SemVer = df.Semver,
                                       IsSigned = userDoc.Customer != null,
                                       DocumentInstanceId = userDoc != null ? userDoc.Id : (Guid?)null,
                                   }).AsNoTracking().ToListAsync();





            List<DocumentInstanceDto> documentInstanceDtos = new();

            foreach (var contractDoc in contractDefinition.ContractDocumentDetails)
            {
                var documentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code).Select(k => k.SemVer).ToArray();
                var findDocumentLastVersion = Versioning.FindLargestVersion(documentsVersionByCode);

                var signedDocumentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && k.IsSigned).Select(k => k.SemVer).ToArray();
                var findUserSignedLastVersion = Versioning.FindLargestVersion(signedDocumentsVersionByCode);

                // Checking contract document min version...
                var customerDocument = documents.FirstOrDefault(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && Versioning.CompareVersion(findUserSignedLastVersion, contractDoc.DocumentDefinition.Semver) && k.IsSigned);

                var documentInstance = new DocumentInstanceDto
                {
                    Code = contractDoc.DocumentDefinition.Code,
                    UseExisting = contractDoc.UseExisting.ToString(),
                    IsRequired = contractDoc.Required,
                    Status = contractDoc.DocumentDefinition.Status.ToString(),
                    MinVersion = contractDoc.DocumentDefinition.Semver,
                    LastVersion = findDocumentLastVersion,
                    Name = contractDoc.DocumentDefinition.Titles.L(req.LangCode),
                    DocumentDetail = new DocumentInstanceDetailDto
                    {
                        OnlineSign = new DocumentInstanceOnlineSignDto
                        {
                            TemplateCode = contractDoc.DocumentDefinition.DocumentOnlineSign.Templates.FirstOrDefault()?.Code, // SORU: Neden liste anlamadım
                            Version = contractDoc.DocumentDefinition.DocumentOnlineSign.Templates.FirstOrDefault()?.Version
                        }
                    }
                };

                if (customerDocument is not null)
                {
                    documentInstance.DocumentInstanceId = customerDocument.DocumentInstanceId;

                    documentInstance.Sign();
                }
                else
                {
                    contractStatus = EStatus.InProgress;
                    // if (documents.Any(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && k.IsSigned))
                    // {
                    //     customerSignedOldVersionDocuments.Add(contractDoc);
                    // }
                }

                documentInstanceDtos.Add(documentInstance);
            }


            var docGroupInstanceDtos = new List<DocumentGroupInstanceDto>();
            foreach (var contractDocGroupDetail in contractDefinition.ContractDocumentGroupDetails)
            {
                var docGroupInstanceDto = new DocumentGroupInstanceDto
                {
                    AtLeastRequiredDocument = contractDocGroupDetail.AtLeastRequiredDocument,
                    Required = contractDocGroupDetail.Required,
                    Status = contractDocGroupDetail.DocumentGroup.Status.ToString(),
                    Title = contractDocGroupDetail.DocumentGroup.Titles.L(req.LangCode)
                };

                foreach (var contractDoc in contractDocGroupDetail.DocumentGroup.DocumentGroupDetails.ToList())
                {

                    var documentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code).Select(k => k.SemVer).ToArray();
                    var findDocumentLastVersion = Versioning.FindLargestVersion(documentsVersionByCode);

                    var signedDocumentsVersionByCode = documents.Where(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && k.IsSigned).Select(k => k.SemVer).ToArray();
                    var findUserSignedLastVersion = Versioning.FindLargestVersion(signedDocumentsVersionByCode);

                    docGroupInstanceDto.DocumentGroupDetailInstanceDto = new DocumentGroupDetailInstanceDto
                    {
                        Code = contractDoc.DocumentGroup.Code,
                        Status = contractDoc.DocumentDefinition.Status.ToString() //SORU doğru mu?
                    };

                    // Checking contract document min version...
                    var customerDocument = documents.FirstOrDefault(k => k.DocumentCode == contractDoc.DocumentDefinition.Code && Versioning.CompareVersion(findUserSignedLastVersion, contractDoc.DocumentDefinition.Semver) && k.IsSigned);


                    var documentInstance = new DocumentInstanceDto
                    {
                        Code = contractDoc.DocumentDefinition.Code,
                        // UseExisting = contractDoc.UseExisting.ToString(), Soru Bunu nereden alıyorduk?
                        IsRequired = contractDocGroupDetail.Required, // SORU Doğru mu?
                        Status = contractDoc.DocumentDefinition.Status.ToString(),
                        MinVersion = contractDoc.DocumentDefinition.Semver,
                        LastVersion = findDocumentLastVersion,
                        Name = contractDoc.DocumentDefinition.Titles.L(req.LangCode),
                        DocumentDetail = new DocumentInstanceDetailDto
                        {
                            OnlineSign = new DocumentInstanceOnlineSignDto
                            {
                                TemplateCode = contractDoc.DocumentDefinition.DocumentOnlineSign.Templates.FirstOrDefault()?.Code, // SORU: Neden liste anlamadım
                                Version = contractDoc.DocumentDefinition.DocumentOnlineSign.Templates.FirstOrDefault()?.Version
                            }
                        }
                    };

                    if (customerDocument is not null)
                    {
                        documentInstance.DocumentInstanceId = customerDocument.DocumentInstanceId;

                        documentInstance.Sign();
                    }
                    else
                    {
                        contractStatus = EStatus.InProgress;
                    }

                    docGroupInstanceDto.DocumentGroupDetailInstanceDto.DocumentInstances.Add(documentInstance);

                }

                docGroupInstanceDtos.Add(docGroupInstanceDto);
            }

            Guid contractInstanceId = req.ContractInstanceId;

            var allDocumentInstanceIds = documentInstanceDtos.Where(d => d.IsSigned && d.DocumentInstanceId.HasValue)
                .Select(d => d.DocumentInstanceId.Value)
            .Concat(docGroupInstanceDtos.Where(k => k.DocumentGroupDetailInstanceDto.DocumentInstances.Any(x => x.IsSigned && x.DocumentInstanceId.HasValue))
                .SelectMany(d => d.DocumentGroupDetailInstanceDto.DocumentInstances)
                    .Select(x => x.DocumentInstanceId.Value)).ToList();

            var userSignedInput = new UserSignedContractInputDto
            {
                ContractCode = req.ContractName,
                ContractInstanceId = contractInstanceId,
                DocumentInstanceIds = allDocumentInstanceIds
            };
            userSignedInput.SetHeaderParameters(req.Reference);

            await _userSignedContractAppService.UpsertAsync(userSignedInput);

            var contractInstanceDto = new ContractInstanceDto()
            {
                ContractCode = contractDefinition.Code,
                ContractInstanceId = contractInstanceId,
                Status = contractStatus.ToString(),
                DocumentList = documentInstanceDtos.Where(k => !k.IsSigned).ToList(),
                DocumentGroupList = docGroupInstanceDtos.Where(k => k.DocumentGroupDetailInstanceDto.DocumentInstances.Any(x => !x.IsSigned)).ToList()
            };
            return GenericResult<ContractInstanceDto>.Success(contractInstanceDto);
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
                Status = EStatus.InProgress.ToString(),
                DocumentList = ObjectMapperApp.Mapper.Map<List<DocumentInstanceDto>>(contractDocumentDetails, opt => opt.Items[Lang.LangCode] = req.LangCode),
                DocumentGroupList = ObjectMapperApp.Mapper.Map<List<DocumentGroupInstanceDto>>(contractDocumentGroupDetails, opt => opt.Items[Lang.LangCode] = req.LangCode)
            };
            if (contractInstanceDto.DocumentList.Count == 0)
                return GenericResult<bool>.Success(true);
            return GenericResult<bool>.Success(false);
        }
    }




}
