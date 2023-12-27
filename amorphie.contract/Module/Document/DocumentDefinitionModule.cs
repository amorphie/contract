using System.Data.SqlTypes;
using amorphie.contract.data.Contexts;
using FluentValidation;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Model.Document;
using amorphie.contract.core.Mapping;
using amorphie.contract.Extensions;

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


        query = ContractHelperExtensions.LikeWhere(query, data.Keyword);
        var documentDefinitions = await query.ToListAsync(token);

        var result = documentDefinitions.Select(d => new
        {
            Code = d.Code,
            Title = d.DocumentDefinitionLanguageDetails
                .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
                .Select(dl => new { dl.MultiLanguage.Name, LanguageType = dl.MultiLanguage.LanguageType.Code })
                .FirstOrDefault() ?? d.DocumentDefinitionLanguageDetails
                    .Select(dl => new { dl.MultiLanguage.Name, LanguageType = dl.MultiLanguage.LanguageType.Code })
                    .FirstOrDefault(),
            Semver = d.Semver
        }).GroupBy(x => new { x.Title.Name, x.Title.LanguageType, x.Code })
          .Select(group => new
          {
              Code = group.Key.Code,
              Title = new { Name = group.Key.Name, LanguageType = group.Key.LanguageType },
              SemverList = group.Select(x => x.Semver).ToList()
          })
          .ToList();

        // var list = await query.ToListAsync(token);
        return Results.Ok(result);
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
            var list = await context.DocumentDefinition.OrderBy(x => x.Code).Skip(page * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync(token);
            var c = list.Select(x =>
         ObjectMapper.Mapper.Map<DocumentDefinitionViewModel>(x)).ToList();
            foreach (var documentDefinitionViewModel in c)
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
            return Results.Ok(c);

        }
        catch (Exception ex)
        {
            Results.Problem(ex.Message);
        }
        return Results.NoContent();

    }
}

