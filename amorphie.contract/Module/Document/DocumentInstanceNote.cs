using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Contexts;
using amorphie.core.Module.minimal_api;

namespace amorphie.contract;

public class DocumentInstanceNoteModule
    : BaseBBTRoute<DocumentInstanceNote, DocumentInstanceNote, ProjectDbContext>
{
    public DocumentInstanceNoteModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "DocumentId", "Note" };
    public override string? UrlFragment => "document-instance-note";
}

