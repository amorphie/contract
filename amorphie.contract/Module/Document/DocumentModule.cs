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
    private readonly IDocumentAppService _documentAppService;
    public DocumentModule(WebApplication app, IDocumentAppService documentAppService) : base(app)
    {
        _documentAppService = documentAppService;
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

    async ValueTask<IResult> getAllDocumentFullTextSearch([AsParameters] PageComponentSearch dataSearch, CancellationToken cancellationToken)
    {
        var inputDto = new GetAllDocumentInputDto
        {
            Keyword = dataSearch.Keyword,
            Page = dataSearch.Page,
            PageSize = dataSearch.PageSize
        };

        var response = await _documentAppService.GetAllDocumentFullTextSearch(inputDto, cancellationToken);

        if (!response.Any())
            return Results.NoContent();

        return Results.Ok(response);
    }

    async ValueTask<IResult> getAllDocumentAll(CancellationToken cancellationToken)
    {
        var response = await _documentAppService.GetAllDocumentAll(cancellationToken);

        if (!response.Any())
            return Results.NoContent();

        return Results.Ok(response);
    }

    async ValueTask<IResponse> Instance(HttpContext httpContext, CancellationToken token, [FromBody] DocumentInstanceInputDto input)
    {

        var response = await _documentAppService.Instance(input);

        return new Response
        {
            Result = response
        };
    }
}






