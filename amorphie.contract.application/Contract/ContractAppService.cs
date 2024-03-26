using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.core.Enum;
using amorphie.contract.infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Response;

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
        public ContractAppService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<GenericResult<bool>> GetExist(ContractGetExistInputDto req, CancellationToken cts)
        {
            var contractDefinition = await _dbContext.ContractDefinition
                .AnyAsync(x => x.Code == req.Code && x.BankEntity == req.EBankEntity, cts);
            return GenericResult<bool>.Success(contractDefinition);
        }

        public async Task<GenericResult<ContractInstanceDto>> Instance(ContractInstanceInputDto req, CancellationToken cts)
        {
            // throw new Exception("");
            //TODO: Daha sonra eklenecek && x.BankEntity == req.EBankEntity
            var contractDefinition = await _dbContext.ContractDefinition.FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);
            // var ss = await _dbContext.ContractDefinition.Include(x=>x.ContractDocumentDetails).FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);
            if (contractDefinition == null)
            {
                throw new ArgumentNullException("not contract");
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

            var contractDocumentDetails = ObjectMapperApp.Mapper.Map<List<ContractDocumentDetailDto>>(listDocument);
            var contractDocumentGroupDetails = ObjectMapperApp.Mapper.Map<List<ContractDocumentGroupDetailDto>>(listDocumentGroup);
            // var contractModel = new ContractDefinitionDto
            // {
            //     Status = EStatus.InProgress.ToString(),
            //     Id = contractDefinition.Id,
            //     Code = contractDefinition.Code,
            //     ContractDocumentDetails = contractDocumentDetails,
            //     ContractDocumentGroupDetails = contractDocumentGroupDetails
            // };

            // if (contractModel.ContractDocumentDetails.Count == 0)
            //     contractModel.Status = EStatus.Completed.ToString();

            ContractRequestHeader.LangCode = req.LangCode ?? "";
            var contractInstanceDto = new ContractInstanceDto()
            {
                Code = contractDefinition.Code,
                Status = EStatus.InProgress.ToString(),
                Document = ObjectMapperApp.Mapper.Map<List<DocumentInstanceDto>>(contractDocumentDetails),
                DocumentGroup = ObjectMapperApp.Mapper.Map<List<DocumentGroupInstanceDto>>(contractDocumentGroupDetails)
            };

             if (contractInstanceDto.Document.Count == 0)
                 contractInstanceDto.Status = EStatus.Completed.ToString();

            //     var a = new ContractInstanceDto();
            //     a.Code = contractModel.Code;
            //     a.Status = contractModel.Status;
            //     a.Document = new List<DocumentInstanceDto>();
            //     foreach (var i in contractDocumentDetails)
            //     {
            //         var dto = new DocumentInstanceDto();
            //         dto.MinVersion = i.MinVersion;
            //         dto.IsRequired = i.Required;
            //         dto.UseExisting = i.UseExisting;
            //         dto.Code = i.DocumentDefinition.Code;
            //         dto.Status = EStatus.InProgress.ToString();
            //         dto.Name = i.DocumentDefinition.MultilanguageText.FirstOrDefault(x => x.Language == req.LangCode)?.Label
            //    ?? i.DocumentDefinition.MultilanguageText.FirstOrDefault()?.Label;

            //         dto.DocumentDetail.OnlineSing = new DocumentInstanceOnlineSingDto
            //         {
            //             TemplateCode = i.DocumentDefinition.DocumentOnlineSing?.DocumentTemplateDetails.FirstOrDefault(x => x.LanguageType == req.LangCode)?.Code
            //                    ?? i.DocumentDefinition.DocumentOnlineSing?.DocumentTemplateDetails.FirstOrDefault()?.Code,
            //             Version = i.DocumentDefinition.DocumentOnlineSing?.DocumentTemplateDetails.FirstOrDefault(x => x.LanguageType == req.LangCode)?.Version
            //                    ?? i.DocumentDefinition.DocumentOnlineSing?.DocumentTemplateDetails.FirstOrDefault()?.Version,
            //         };


            //         a.Document.Add(dto);

            //     }

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

            var contractDocumentDetails = ObjectMapperApp.Mapper.Map<List<ContractDocumentDetailDto>>(listDocument);

            var contractModel = new ContractDefinitionDto
            {
                Status = EStatus.InProgress.ToString(),
                Id = contractDefinition.Id,
                Code = contractDefinition.Code,
                ContractDocumentDetails = contractDocumentDetails,
                ContractDocumentGroupDetails = ObjectMapperApp.Mapper.Map<List<ContractDocumentGroupDetailDto>>(listDocumentGroup)
            };
            if (contractModel.ContractDocumentDetails.Count == 0)
                return GenericResult<bool>.Success(true);


            // return false;
            return GenericResult<bool>.Success(false);
        }
    }
}
