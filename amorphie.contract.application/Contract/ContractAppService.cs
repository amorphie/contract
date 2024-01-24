using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.Contract;
using amorphie.contract.core.Services;
using amorphie.contract.data.Contexts;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract
{
    public interface IContractAppService
    {
        Task<ContractDto> Instance(ContractRequest req, string language, CancellationToken cts);
    }
    public class ContractAppService : IContractAppService
    {
        private readonly ProjectDbContext _dbContext;
        public ContractAppService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ContractDto> Instance(ContractRequest req, string language, CancellationToken cts)
        {
            var contractDefinition = await _dbContext.ContractDefinition.FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);
            if (contractDefinition == null)
            {
                return new ContractDto { Status = "not contract" };
            }
            //contractModel.Status = "in-progress";
            //contractModel.Id = query.Id;
            //contractModel.Code = query.Code;

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

            var listModel = listDocument.Select(x => ObjectMapperApp.Mapper.Map<DocumentDto>(x)).ToList();

            var listModelGroup = listDocumentGroup.Select(a =>
                new DocumentGroupDto
                {
                    Title = a.DocumentGroup.DocumentGroupLanguageDetail
                        .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
                        .FirstOrDefault()?.MultiLanguage?.Name ?? a.DocumentGroup.DocumentGroupLanguageDetail.FirstOrDefault().MultiLanguage.Name,
                    Status = a.AtLeastRequiredDocument <= a.DocumentGroup.DocumentGroupDetails
                    .Where(c => customerDocumentGroup.Contains(c.DocumentDefinitionId)).Count() ? EStatus.Completed.ToString() : EStatus.InProgress.ToString(),

                    AtLeastRequiredDocument = a.AtLeastRequiredDocument,

                    Required = a.Required,
                    Document = a.DocumentGroup.DocumentGroupDetails
                    .Where(c => !customerDocumentGroup.Contains(c.DocumentDefinitionId)).Select(x => ObjectMapperApp.Mapper.Map<DocumentDto>(x)).ToList()
                }

            ).ToList();
            var contractModel = new ContractDto
            {
                Status = "in-progress",
                Id = contractDefinition.Id,
                Code = contractDefinition.Code,
                Document = listModel,
                DocumentGroups = listModelGroup
            };

            if (contractModel.Document.Count == 0)
                contractModel.Status = "valid";

            return contractModel;
        }
    }
}
