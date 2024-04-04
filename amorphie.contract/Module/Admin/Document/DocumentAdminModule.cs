using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentAdminModule
    : AudiAdminModule<core.Entity.Document.Document, core.Entity.Document.Document, ProjectDbContext>
{
    public DocumentAdminModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "DocumentContentId" };

    public override string? UrlFragment => base.UrlFragment + "document";

}






