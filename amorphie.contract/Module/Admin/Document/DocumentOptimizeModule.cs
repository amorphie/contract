using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentOptimizeModule
    : AudiAdminModule<DocumentOptimize, DocumentOptimize, ProjectDbContext>
{
    public DocumentOptimizeModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Size", "Transform" };
    public override string? UrlFragment => base.UrlFragment + "document-optimize";
}

