using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Contexts;
using amorphie.core.Module.minimal_api;

namespace amorphie.contract;

    public class DocumentGroupHistoryModule
        : BaseBBTRoute<DocumentTsizl, DocumentTsizl, ProjectDbContext>
{
    public DocumentGroupHistoryModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "History", "DocumentDefinitionId" };

    public override string? UrlFragment => "document-group-history";



}