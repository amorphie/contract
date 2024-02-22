using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using amorphie.contract.application;
using amorphie.core.IBase;
using amorphie.contract.core.Services;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract;

public class DocumentModule
    : BaseBBTRoute<RootDocumentDto, Document, ProjectDbContext>
{
    public DocumentModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "DocumentContentId" };

    public override string? UrlFragment => "document";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("search", getAllDocumentFullTextSearch);
        routeGroupBuilder.MapGet("getAll", getAllDocumentAll);
        routeGroupBuilder.MapPost("Instance", Instance);
        routeGroupBuilder.MapGet("download", DownloadDocument);

    }

    async ValueTask<IResult> getAllDocumentFullTextSearch([FromServices] IDocumentAppService documentAppService, [AsParameters] PageComponentSearch dataSearch, CancellationToken cancellationToken)
    {
        var inputDto = new GetAllDocumentInputDto
        {
            Keyword = dataSearch.Keyword,
            Page = dataSearch.Page,
            PageSize = dataSearch.PageSize
        };

        var response = await documentAppService.GetAllDocumentFullTextSearch(inputDto, cancellationToken);

        if (!response.Any())
            return Results.NoContent();

        return Results.Ok(response);
    }

    async ValueTask<IResult> getAllDocumentAll([FromServices] IDocumentAppService documentAppService, CancellationToken cancellationToken)
    {
        var response = await documentAppService.GetAllDocumentAll(cancellationToken);

        if (!response.Any())
            return Results.NoContent();

        return Results.Ok(response);
    }

    async ValueTask<IResponse> Instance([FromServices] IDocumentAppService documentAppService, HttpContext httpContext,
    CancellationToken token, [FromBody] DocumentInstanceInputDto input)
    {
        var headerModels = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;
        input.SetHeaderParameters(headerModels);
        var response = await documentAppService.Instance(input);

        return new Response
        {
            Result = response
        };
    }

    async ValueTask DownloadDocument([FromServices] IDocumentAppService documentAppService, HttpContext httpContext, [AsParameters] DocumentDownloadInputDto inputDto, CancellationToken token)
    {
        var headerModels = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;
        inputDto.SetUserReference(headerModels.UserReference);

        var doc = await documentAppService.DownloadDocument(inputDto, token);
        httpContext.Response.ContentType = doc.ContentType;
        await doc.Stream.CopyToAsync(httpContext.Response.Body);
    }
}






