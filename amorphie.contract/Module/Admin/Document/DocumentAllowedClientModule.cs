using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentAllowedClientModule
    : BaseAdminModule<DocumentAllowedClient, DocumentAllowedClient, ProjectDbContext>
{
    public DocumentAllowedClientModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Code" };
    public override string? UrlFragment => base.UrlFragment + "document-allowed-client";
}

