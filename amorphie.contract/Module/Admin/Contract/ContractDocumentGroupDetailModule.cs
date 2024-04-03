using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Contract;

public class ContractDocumentGroupDetailModule
    : AudiAdminModule<ContractDocumentGroupDetail, ContractDocumentGroupDetail, ProjectDbContext>
{
    public ContractDocumentGroupDetailModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "ContractDefinitionId", "DocumentDefinitionId" };
    public override string? UrlFragment => base.UrlFragment +"contract-document-group-detail";
}

