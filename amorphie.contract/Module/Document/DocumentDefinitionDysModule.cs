using amorphie.contract.application;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.data.Contexts;
using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;

namespace amorphie.contract;

    public class DocumentDefinitionDysModule
        : BaseBBTRoute<DocumentDefinitionDys, DocumentDefinitionDys, ProjectDbContext>
    {
        public DocumentDefinitionDysModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"ReferenceId","DocumentDefinitionId"};

        public override string? UrlFragment => "document-definition-dys";

        public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
        {
            base.AddRoutes(routeGroupBuilder);
            routeGroupBuilder.MapGet("DmsTagList", TagList);

        }

        async ValueTask<IResult> TagList([FromServices] IDocumentDefinitionDysAppService documentDefinitionDysService, HttpContext httpContext,
            CancellationToken token, [FromQuery] string referenceId)
        {
            var response = documentDefinitionDysService.GetAllTagsDys(referenceId,token);

            return Results.Ok(new
            {
                Data = response,
                Success = true,
                ErrorMessage = "",
            });
        }
       
    }