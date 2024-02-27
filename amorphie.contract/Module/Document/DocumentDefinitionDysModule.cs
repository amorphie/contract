using amorphie.contract.core.Entity.Document;
using amorphie.contract.data.Contexts;
using amorphie.core.Module.minimal_api;

namespace amorphie.contract;

    public class DocumentDefinitionDysModule
        : BaseBBTRoute<DocumentDefinitionDys, DocumentDefinitionDys, ProjectDbContext>
    {
        public DocumentDefinitionDysModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"ReferenceId","DocumentDefinitionId"};

        public override string? UrlFragment => "document-definition-dys";


       
    }