using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Mapping;
using amorphie.contract.core.Model.Contract;
using amorphie.contract.core.Model.Document;
using amorphie.contract.data.Contexts;

namespace amorphie.contract.data.Services
{
    public interface IContractInstanceService
    {
        Task<ContractModel> Start(Contract contract, string Language);

    }
    public class ContractInstanceService : IContractInstanceService
    {
        private readonly ProjectDbContext _context;
        private Contract _data;
        private string _language;
        public ContractInstanceService(ProjectDbContext context)
        {
            _context = context;
        }
        ContractModel contractModel = new ContractModel();
        private void ControlContractDefinition()
        {
            var query = _context.ContractDefinition.FirstOrDefault(x => x.Code == _data.ContractName);
            if (query == null)
            {
                new Exception("Contract model not found");
            }
            else
            {
                contractModel = ObjectMapper.Mapper.Map<ContractModel>(query);

            }
        }
        public async Task<ContractModel> Start(Contract data, string language)
        {
            // _language = language;
            // _data = data;
            // var query = _context.ContractDefinition.FirstOrDefault(x => x.Code == data.ContractName);
            // if (query == null)
            // {
            //     contractModel.Status = "not contract";
            //     return contractModel;
            // }
            // contractModel.Status = "in-progress";
            // contractModel.Id = query.Id;
            // contractModel.Code = query.Code;

            // var documentList = query.ContractDocumentDetails.
            //                     Select(x => x.DocumentDefinitionId)
            //                     .ToList();

            // var customerDocument = _context.Document.Where(x => x.Customer.Reference == data.Reference &&
            //                 documentList.Contains(x.DocumentDefinitionId)).ToList()
            //                 .Select(x => x.DocumentDefinitionId).ToList();


            // var list = query.ContractDocumentDetails.
            // Where(d => !customerDocument.Contains(d.DocumentDefinitionId));


            // var listModel = list.Select(x => new DocumentModel
            // {
            //     Title = x.DocumentDefinition.DocumentDefinitionLanguageDetails
            //         .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
            //         .FirstOrDefault()?.MultiLanguage?.Name ?? x.DocumentDefinition.DocumentDefinitionLanguageDetails.FirstOrDefault().MultiLanguage.Name,

            //     Code = x.DocumentDefinition.Code,
            //     Status = "not-started",
            //     Required = x.Required,
            //     Render = x.DocumentDefinition.DocumentOnlineSing != null,
            //     Version = x.DocumentDefinition.Semver,
            //     OnlineSign = new OnlineSignModel
            //     {
            //         DocumentModelTemplate = x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == language).Count() > 0 ?
            //          x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == language).Select(b => new DocumentModelTemplate
            //          {
            //              Name = b.DocumentTemplate.Code,
            //              MinVersion = b.DocumentTemplate.Version,
            //          }).ToList() : x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Select(b => new DocumentModelTemplate
            //          {
            //              Name = b.DocumentTemplate.Code,
            //              MinVersion = b.DocumentTemplate.Version,
            //          }).Take(1).ToList(),

            //         ScaRequired = x.DocumentDefinition.DocumentOnlineSing != null ? x.DocumentDefinition.DocumentOnlineSing.Required : false,
            //         AllovedClients = x.DocumentDefinition.DocumentOnlineSing.DocumentAllowedClientDetails
            //                                     .Select(x => x.DocumentAllowedClients.Code)
            //                                     .ToList() ?? new List<string>()
            //     }
            // }
            // ).ToList();
            // contractModel.Document = listModel;

            // if (contractModel.Document.Count == 0)
            // {
            //     contractModel.Status = "valid";
            // }


            return contractModel;
        }


    }


}