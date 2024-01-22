using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using amorphie.contract.application;
using amorphie.core.IBase;

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
    }

    async ValueTask<IResult> getAllDocumentFullTextSearch([FromServices]IDocumentAppService documentAppService, [AsParameters] PageComponentSearch dataSearch, CancellationToken cancellationToken)
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

    async ValueTask<IResult> getAllDocumentAll([FromServices]IDocumentAppService documentAppService, CancellationToken cancellationToken)
    {
        var response = await documentAppService.GetAllDocumentAll(cancellationToken);

        if (!response.Any())
            return Results.NoContent();

        return Results.Ok(response);
    }

    async ValueTask<IResponse> Instance([FromServices]IDocumentAppService documentAppService, CancellationToken token, [FromBody] DocumentInstanceInputDto input)
    {

        var response = await documentAppService.Instance(input);

        return new Response
        {
            Result = response
        };
    }
}






