using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentContentModule
    : AudiAdminModule<DocumentContent, DocumentContent, ProjectDbContext>
{
    public DocumentContentModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "ContentData", "DocumentVersionsId" };
    public override string? UrlFragment => base.UrlFragment + "document-content";
}

