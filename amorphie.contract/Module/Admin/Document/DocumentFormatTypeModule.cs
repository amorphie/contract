using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.Module.Base;
namespace amorphie.contract.Module.Admin.Document;

public class DocumentFormatTypeModule :
    BaseAdminModule<DocumentFormatType, DocumentFormatType, ProjectDbContext>
{
    public DocumentFormatTypeModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Name", "ContentType" };
    public override string? UrlFragment => base.UrlFragment + "document-format-type";
}

