using System.IO.Compression;

using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Model;
using Google.Rpc;

namespace amorphie.contract;

public class DocumentDefinitionModule
    : BaseBBTRoute<DocumentDefinition, DocumentDefinition, ProjectDbContext>
{
    public DocumentDefinitionModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Code", "StatusId" };

    public override string? UrlFragment => "document-definition";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("getAnyDocumentDefinitionListSearch", getAnyDocumentDefinitionListSearch);
    }
    async ValueTask<IResult> getAnyDocumentDefinitionListSearch(
        [FromServices] ProjectDbContext context, [AsParameters] ComponentSearch dataSearch,
        CancellationToken cancellationToken
   )
    {
        try
        {

            var query = context!.DocumentDefinition.AsQueryable();
            var anyValue = false;
            if (!string.IsNullOrEmpty(dataSearch.Keyword))
            {
                anyValue = query.Any(x => dataSearch.Keyword == x.Code);
            }

            return Results.Ok(anyValue);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return Results.NoContent();
    }

}

