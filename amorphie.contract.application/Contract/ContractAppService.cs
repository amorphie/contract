using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.core.Enum;
using amorphie.contract.data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.application.Contract
{
    public interface IContractAppService
    {
        Task<ContractDefinitionDto> Instance(ContractInstaceInputDto req, CancellationToken cts);
    }
    public class ContractAppService : IContractAppService
    {
        private readonly ProjectDbContext _dbContext;
        public ContractAppService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ContractDefinitionDto> Instance(ContractInstaceInputDto req, CancellationToken cts)
        {
            var contractDefinition = await _dbContext.ContractDefinition.FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);
            if (contractDefinition == null)
            {
                return new ContractDefinitionDto { Status = "not contract" };
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

            var listModel = ObjectMapperApp.Mapper.Map<List<ContractDocumentDetailDto>>(listDocument);

            //TODO: Umut - mapping sonra yapılacak.
            // var listModelGroup = listDocumentGroup.Select(a =>
            //     new DocumentGroupDto
            //     {
            //         Title = a.DocumentGroup.DocumentGroupLanguageDetail
            //             .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
            //             .FirstOrDefault()?.MultiLanguage?.Name ?? a.DocumentGroup.DocumentGroupLanguageDetail.FirstOrDefault().MultiLanguage.Name,
            //         Status = a.AtLeastRequiredDocument <= a.DocumentGroup.DocumentGroupDetails
            //         .Where(c => customerDocumentGroup.Contains(c.DocumentDefinitionId)).Count() ? EStatus.Completed.ToString() : EStatus.InProgress.ToString(),

            //         AtLeastRequiredDocument = a.AtLeastRequiredDocument,

            //         Required = a.Required,
            //         Document = a.DocumentGroup.DocumentGroupDetails
            //         .Where(c => !customerDocumentGroup.Contains(c.DocumentDefinitionId)).Select(x => ObjectMapperApp.Mapper.Map<DocumentDto>(x)).ToList()
            //     }
            // ).ToList();

            var contractModel = new ContractDefinitionDto
            {
                Status = AppConsts.InProgress,
                Id = contractDefinition.Id,
                Code = contractDefinition.Code,
                ContractDocumentDetails = listModel,
                // DocumentGroups = listModelGroup
            };

            if (contractModel.ContractDocumentDetails.Count == 0)
                contractModel.Status = AppConsts.Valid;

            return contractModel;
        }
    }
}
