using amorphie.contract.core.Entity.Contract;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Contract;

public class ContractDefinitionHistoryModule
        : AudiAdminModule<ContractDefinitionHistory, ContractDefinitionHistory, ProjectDbContext>
{
    public ContractDefinitionHistoryModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "History", "DocumentDefinitionId" };
    public override string? UrlFragment => base.UrlFragment + "contract-definition-history";
}