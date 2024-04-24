using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentFormatModule
    : AudiAdminModule<DocumentFormat, DocumentFormat, ProjectDbContext>
{
    public DocumentFormatModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "DocumentTypeId", "DocumentSizeId" };
    public override string? UrlFragment => base.UrlFragment + "document-format";
}

