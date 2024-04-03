using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;
namespace amorphie.contract.Module.Admin.Document;

public class DocumentOperationsModule
    : AudiAdminModule<DocumentOperations, DocumentOperations, ProjectDbContext>
{
    public DocumentOperationsModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "TagId" };
    public override string? UrlFragment => base.UrlFragment + "document-Operations";
}

