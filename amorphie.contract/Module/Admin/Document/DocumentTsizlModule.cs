using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentTsizlModule
    : AudiAdminModule<DocumentTsizl, DocumentTsizl, ProjectDbContext>
{
    public DocumentTsizlModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "EngagementKind", "DocumentDefinitionId" };
    public override string? UrlFragment => base.UrlFragment + "document-tsizl";
}