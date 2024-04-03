using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Contract;

public class ContractEntityPropertyModule
    : AudiAdminModule<ContractEntityProperty, ContractEntityProperty, ProjectDbContext>
{
    public ContractEntityPropertyModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "ContractDefinitionId", "EntityPropertyId" };
    public override string? UrlFragment => base.UrlFragment + "contract-entity-property";
}

