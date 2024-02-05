using amorphie.contract.data.Contexts;
using FluentValidation;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.Extensions;
using amorphie.contract.application;
using amorphie.core.Extension;
using amorphie.contract.application.Extensions;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract;

public class DocumentDefinitionModule
    : BaseBBTContractRoute<DocumentDefinitionDto, DocumentDefinition, ProjectDbContext>
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

    protected async override ValueTask<IResult> GetAllMethod([FromServices] ProjectDbContext context, [FromServices] IMapper mapper, [FromQuery, Range(0, 100)] int page, [FromQuery, Range(5, 100)] int pageSize, HttpContext httpContext, CancellationToken token, [FromQuery] string? sortColumn, [FromQuery] SortDirectionEnum? sortDirection)
    {

        var headerModels = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;

        var input = new GetAllDocumentDefinitionInputDto
        {
            LangCode = headerModels.LangCode,
            Page = page,
            PageSize = pageSize
        };

        var list = await context.DocumentDefinition.OrderBy(x => x.Code).Skip(input.Page * input.PageSize).Take(input.PageSize).AsNoTracking().ToListAsync(token);

        var responseDtos = ObjectMapperApp.Mapper.Map<List<DocumentDefinitionDto>>(list);

        foreach (var dto in responseDtos)
        {
            if (dto.MultilanguageText is not null)
            {
                var selectedLanguageText = dto?.MultilanguageText.FirstOrDefault(t => t.Language == input.LangCode);

                if (selectedLanguageText != null)
                {
                    dto.Name = selectedLanguageText.Label;
                }
                else if (dto.MultilanguageText.Any())
                {
                    dto.Name = dto.MultilanguageText.First().Label;
                }
            }

        }



        if (responseDtos is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(responseDtos);

    }


}

