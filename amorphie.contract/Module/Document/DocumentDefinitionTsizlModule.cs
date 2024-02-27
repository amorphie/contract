using amorphie.contract.core.Entity.Document;
using amorphie.contract.data.Contexts;
using amorphie.core.Module.minimal_api;

namespace amorphie.contract;

    public class DocumentDefinitionTsizlModule
        : BaseBBTRoute<DocumentDefinitionTsizl, DocumentDefinitionTsizl, ProjectDbContext>
    {
        public DocumentDefinitionTsizlModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"EngagementKind","DocumentDefinitionId"};

        public override string? UrlFragment => "document-definition-tsizl";


       
    }