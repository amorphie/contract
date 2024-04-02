using amorphie.contract.infrastructure.Contexts;
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
using amorphie.contract.core.Extensions;

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

        var result =  documentDefinitions
            .Select(d => new
            {
                Code = d.Code,
                Title = d.Titles,
                Semver = d.Semver
            })
            .GroupBy(x => new { x.Title, language, x.Code })
            .Select(group => new
            {
                Code = group.Key.Code,
                Title = new { Name = group.Key.Title.L(language), LanguageType = language },
                SemverList = group.Select(x => x.Semver).ToList()
            })
            .ToList();

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

        var langCode = HeaderHelper.GetHeaderLangCode(httpContext);

        var input = new GetAllDocumentDefinitionInputDto
        {
            LangCode = langCode,
            Page = page,
            PageSize = pageSize
        };

        var list = await context.DocumentDefinition.OrderBy(x => x.Code).Skip(input.Page * input.PageSize).Take(input.PageSize).AsNoTracking().ToListAsync(token);

        var responseDtos = ObjectMapperApp.Mapper.Map<List<DocumentDefinitionDto>>(list, opt => opt.Items[Lang.LangCode] = input.LangCode).ToList();

        if (responseDtos is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(responseDtos);

    }


}

