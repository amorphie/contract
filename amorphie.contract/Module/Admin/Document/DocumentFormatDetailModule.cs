using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;
namespace amorphie.contract.Module.Admin.Document;

public class DocumentFormatDetailModule
    : AudiAdminModule<DocumentFormatDetail, DocumentFormatDetail, ProjectDbContext>
{
    public DocumentFormatDetailModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "DocumentFormatId" };
    public override string? UrlFragment => base.UrlFragment + "document-format-detail";
}

