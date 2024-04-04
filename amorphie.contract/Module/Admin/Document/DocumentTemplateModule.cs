using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentTemplateModule
    : AudiAdminModule<DocumentTemplate, DocumentTemplate, ProjectDbContext>
{
    public DocumentTemplateModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Name" };
    public override string? UrlFragment => base.UrlFragment + "document-template";
}

