using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentSizeModule
    : AudiAdminModule<DocumentSize, DocumentSize, ProjectDbContext>
{
    public DocumentSizeModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "KiloBytes" };
    public override string? UrlFragment => base.UrlFragment + "document-size";
}

