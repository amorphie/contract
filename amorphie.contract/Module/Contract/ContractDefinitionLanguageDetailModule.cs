using amorphie.contract.core;
using amorphie.contract.data.Contexts;
using amorphie.core.Module.minimal_api;

namespace amorphie.contract;

public class ContractDefinitionLanguageDetailModule
        : BaseBBTRoute<ContractDefinitionLanguageDetail, ContractDefinitionLanguageDetail, ProjectDbContext>
    {
        public ContractDefinitionLanguageDetailModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"LanguageId","ContractDefinitionId"};

        public override string? UrlFragment => "contract-definition-language-detail";



    }