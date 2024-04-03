using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Contexts;
using amorphie.core.Module.minimal_api;

namespace amorphie.contract;

    public class ContractDefinitionHistoryModule
        : BaseBBTRoute<ContractDefinitionHistory, ContractDefinitionHistory, ProjectDbContext>
{
    public ContractDefinitionHistoryModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "History", "DocumentDefinitionId" };

    public override string? UrlFragment => "contract-definition-history";



}