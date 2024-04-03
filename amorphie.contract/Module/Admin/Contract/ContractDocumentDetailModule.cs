using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Contract;

public class ContractDocumentDetailModule
    : AudiAdminModule<ContractDocumentDetail, ContractDocumentDetail, ProjectDbContext>
{
    public ContractDocumentDetailModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "ContractDefinitionId", "DocumentDefinitionId" };
    public override string? UrlFragment => base.UrlFragment + "contract-document-detail";




}

