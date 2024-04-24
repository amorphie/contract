using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentInstanceEntityPropertyModule
    : AudiAdminModule<DocumentInstanceEntityProperty, DocumentInstanceEntityProperty, ProjectDbContext>
{
    public DocumentInstanceEntityPropertyModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "DocumentId", "EntityPropertyId" };
    public override string? UrlFragment => base.UrlFragment + "document-instance-entity-property";
}

