using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentTemplateDetailModule
    : AudiAdminModule<DocumentTemplateDetail, DocumentTemplateDetail, ProjectDbContext>
{
    public DocumentTemplateDetailModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId" };
    public override string? UrlFragment => base.UrlFragment + "document-template-detail";
}

