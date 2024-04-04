using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document;

public class DocumentInstanceNoteModule
    : AudiAdminModule<DocumentInstanceNote, DocumentInstanceNote, ProjectDbContext>
{
    public DocumentInstanceNoteModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "DocumentId", "Note" };
    public override string? UrlFragment => "document-instance-note";
}

