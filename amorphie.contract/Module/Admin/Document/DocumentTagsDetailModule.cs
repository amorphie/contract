using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentTagsDetailModule
    : AudiAdminModule<DocumentTagsDetail, DocumentTagsDetail, ProjectDbContext>
{
    public DocumentTagsDetailModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "TagId", "DocumentDefinitionId" };
    public override string? UrlFragment => base.UrlFragment + "document-tag-detail";
}

