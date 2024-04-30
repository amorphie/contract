using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Contract;

public class ContractValidationModule
    : AudiAdminModule<ContractValidation, ContractValidation, ProjectDbContext>
{
    public ContractValidationModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "ContractDefinitionId", "ValidationId" };
    public override string? UrlFragment => base.UrlFragment + "contract-Validation";
}

