using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentOptimizeTypeModule
    : BaseAdminModule<DocumentOptimizeType, DocumentOptimizeType, ProjectDbContext>
{
    public DocumentOptimizeTypeModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Code" };
    public override string? UrlFragment => base.UrlFragment + "document-Optimize-Type";
}

