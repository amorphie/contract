using System.Data.SqlTypes;
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
using amorphie.contract.core.Model.Document;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Data.Common;
using amorphie.contract.core.Mapping;
using Google.Protobuf.WellKnownTypes;

namespace amorphie.contract;

public class DocumentDefinitionModule
    : BaseBBTContractRoute<DocumentDefinition, DocumentDefinition, ProjectDbContext>
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

        if (!string.IsNullOrEmpty(data.Keyword) && data.Keyword != "*")
        {
            // "%" karakteri varsa deseni kontrol et
            if (data.Keyword.Contains("%"))
            {
                // "%" karakterini kaldır
                string pattern = data.Keyword.Replace("%", "");

                // Deseni kontrol et
                query = query.Where(x => x.Code.StartsWith(pattern) || x.Code.EndsWith(pattern));
            }
            else
            {
                // "%" karakteri yoksa direkt olarak eşleşmeyi kontrol et
                query = query.Where(x => x.Code == data.Keyword);
            }
        }

        var list = await query.Select(x => new
        {
            x.Id,
            x.Code,
            title = x.DocumentDefinitionLanguageDetails!.Any(a => a.MultiLanguage.LanguageType.Code == language) ?
            x.DocumentDefinitionLanguageDetails!.Where(a => a.MultiLanguage.LanguageType.Code == language)
            .Select(x => new { x.MultiLanguage.Name, LanguageType = x.MultiLanguage.LanguageType.Code }).FirstOrDefault() :
            x.DocumentDefinitionLanguageDetails!
            .Select(x => new { x.MultiLanguage.Name, LanguageType = x.MultiLanguage.LanguageType.Code }).FirstOrDefault(),
            x.Semver
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

    protected override async ValueTask<IResult> GetAllMethod([FromServices] ProjectDbContext context, [FromServices] IMapper mapper,
       [FromQuery][Range(0, 100)] int page, [FromQuery][Range(5, 100)] int pageSize, HttpContext httpContext, CancellationToken token)
    {

        try
        {
            var language = httpContext.Request.Headers["Language"].ToString();
            if (string.IsNullOrEmpty(language))
            {
                language = "en-EN";
            }
            var list = await context!.DocumentDefinition!.Select(x =>
            ObjectMapper.Mapper.Map<DocumentDefinitionViewModel>(x)).Skip(page * pageSize)
                .Take(pageSize).ToListAsync(token);
            foreach (var documentDefinitionViewModel in list)
            {
                var selectedLanguageText = documentDefinitionViewModel?.MultilanguageText
                    .FirstOrDefault(t => t.Language == language);

                if (selectedLanguageText != null)
                {
                    documentDefinitionViewModel.Name = selectedLanguageText.Label;
                }
                else if (documentDefinitionViewModel.MultilanguageText.Any())
                {
                    documentDefinitionViewModel.Name = documentDefinitionViewModel.MultilanguageText.First().Label;
                }
            }
            return Results.Ok(list);

        }
        catch (Exception ex)
        {
            Results.Problem(ex.Message);
        }
        return Results.NoContent();

    }
}

