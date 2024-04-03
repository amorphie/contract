using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document.DocumentGroups;

public class DocumentGroupHistoryModule
    : AudiAdminModule<DocumentTsizl, DocumentTsizl, ProjectDbContext>
{
    public DocumentGroupHistoryModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "History", "DocumentDefinitionId" };
    public override string? UrlFragment => base.UrlFragment +"document-group-history";
}