using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Contract;

public class ContractTagModule
    : AudiAdminModule<ContractTag, ContractTag, ProjectDbContext>
{
    public ContractTagModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "ContractDefinitionId", "TagId " };
    public override string? UrlFragment => base.UrlFragment + "contract-tag";
}

