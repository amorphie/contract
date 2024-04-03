using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentEntityPropertyModule
    : AudiAdminModule<DocumentEntityProperty, DocumentEntityProperty, ProjectDbContext>
{
    public DocumentEntityPropertyModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "EntityPropertyId" };
    public override string? UrlFragment => base.UrlFragment + "document-entity-property";
}

