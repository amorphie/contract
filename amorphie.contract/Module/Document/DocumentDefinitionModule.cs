using System.Net;
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
using AutoMapper;
using Refit;
using Microsoft.EntityFrameworkCore;

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
        routeGroupBuilder.MapGet("GetAllSearch", getAllSearch);
    }
    async ValueTask<IResult> getAllSearch([FromServices] ProjectDbContext context, [FromServices] IMapper mapper,
    HttpContext httpContext, CancellationToken token, [AsParameters] ComponentSearch data,
   [FromHeader(Name = "Language")] string? language = "en-EN")
    {
        var query = context!.DocumentDefinition.AsQueryable();

        if (data != null)
        {
            data.Keyword = data.Keyword.Trim();
            if (!string.IsNullOrEmpty(data.Keyword.Trim()) && data.Keyword != "*"&& data.Keyword != "string")
            {
                query = query.Where(x => x.Code.Contains(data.Keyword));
            }
        }
        var list = await query.Select(x => new
        {
            x.Id,
            x.Code,
            title = x.DocumentDefinitionLanguageDetails.Any(a => a.MultiLanguage.LanguageType.Code == language) ?
            x.DocumentDefinitionLanguageDetails.Where(a => a.MultiLanguage.LanguageType.Code == language)
            .Select(x => new { x.MultiLanguage.Name, LanguageType = x.MultiLanguage.LanguageType.Code }).FirstOrDefault() :
            x.DocumentDefinitionLanguageDetails.Where(a => a.MultiLanguage.LanguageType.Code == "en-EN")
            .Select(x => new { x.MultiLanguage.Name, LanguageType = x.MultiLanguage.LanguageType.Code }).FirstOrDefault(),

        }).ToListAsync(token);
        // var list = await query.ToListAsync(token);
        return Results.Ok(list);
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

