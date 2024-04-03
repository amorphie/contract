using amorphie.contract.application;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Contexts;
using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;

namespace amorphie.contract;

    public class DocumentDysModule
        : BaseBBTRoute<DocumentDys, DocumentDys, ProjectDbContext>
{
    public DocumentDysModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "ReferenceId", "DocumentDefinitionId" };

    public override string? UrlFragment => "document-dys";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("DmsTagList", TagList);

    }

    async ValueTask<IResult> TagList([FromServices] IDocumentDysAppService documentDysService, HttpContext httpContext,
        CancellationToken token, [FromQuery] int referenceId)
    {
        var response = documentDysService.GetAllTagsDys(referenceId, token);

        return Results.Ok(new
        {
            Data = response,
            Success = true,
            ErrorMessage = "",
        });
    }

}