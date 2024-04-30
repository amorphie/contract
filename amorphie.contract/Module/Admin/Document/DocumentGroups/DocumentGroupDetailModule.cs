using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document.DocumentGroups;

public class DocumentGroupDetailModule
    : AudiAdminModule<DocumentGroupDetail, DocumentGroupDetail, ProjectDbContext>
{
    public DocumentGroupDetailModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "DocumentGroupId" };
    public override string? UrlFragment => base.UrlFragment + "document-group-detail";
}

