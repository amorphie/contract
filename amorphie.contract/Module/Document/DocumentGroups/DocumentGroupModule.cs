
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Model;

namespace amorphie.contract;

public class DocumentGroupModule
    : BaseBBTRoute<DocumentGroup, DocumentGroup, ProjectDbContext>
{
    public DocumentGroupModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Code", "Name" };

    public override string? UrlFragment => "document-group";
    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("getAnyDocumentGroupListSearch", getAnyDocumentGroupListSearch);
    }
    async ValueTask<IResult> getAnyDocumentGroupListSearch(
            [FromServices] ProjectDbContext context, [AsParameters] ComponentSearch dataSearch,
            CancellationToken cancellationToken
        )
    {
        try
        {

            var query = context!.DocumentGroup.AsQueryable();
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

