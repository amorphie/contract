using amorphie.contract.data.Contexts;

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.Extensions;
using amorphie.contract.application;
using amorphie.core.Extension;
using amorphie.contract.application.Extensions;

namespace amorphie.contract;

public class DocumentGroupModule
    : BaseBBTContractRoute<DocumentGroup, DocumentGroup, ProjectDbContext>
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
        routeGroupBuilder.MapGet("GetAllSearch", getAllSearch);
    }
    async ValueTask<IResult> getAllSearch([FromServices] ProjectDbContext context, [FromServices] IMapper mapper,
   HttpContext httpContext, CancellationToken token, [AsParameters] ComponentSearch data,
  [FromHeader(Name = "Language")] string? language = "en-EN")
    {
        var query = context!.DocumentGroup.AsQueryable();

        query = ContractHelperExtensions.LikeWhere(query, data.Keyword);
        var list = await query.Select(x => new
        {
            x.Id,
            x.Code,
            title = x.DocumentGroupLanguageDetail.Any(a => a.MultiLanguage.LanguageType.Code == language) ?
            x.DocumentGroupLanguageDetail.Where(a => a.MultiLanguage.LanguageType.Code == language)
            .Select(x => new { x.MultiLanguage.Name, LanguageType = x.MultiLanguage.LanguageType.Code }).FirstOrDefault() :
            x.DocumentGroupLanguageDetail.Where(a => a.MultiLanguage.LanguageType.Code == "en-EN")
            .Select(x => new { x.MultiLanguage.Name, LanguageType = x.MultiLanguage.LanguageType.Code }).FirstOrDefault(),

        }).ToListAsync(token);
        // var list = await query.ToListAsync(token);
        return Results.Ok(list);
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

    protected async override ValueTask<IResult> GetAllMethod([FromServices] ProjectDbContext context, [FromServices] IMapper mapper, [FromQuery, Range(0, 100)] int page, [FromQuery, Range(5, 100)] int pageSize, HttpContext httpContext, CancellationToken token, [FromQuery] string? sortColumn, [FromQuery] SortDirectionEnum? sortDirection)
    {
        try
        {
            var language = httpContext.Request.Headers["Language"].ToString();
            if (string.IsNullOrEmpty(language))
            {
                language = "en-EN";
            }

            var list = await context!.DocumentGroup!.Skip(page * pageSize).Take(pageSize).ToListAsync(token);

            var response = ObjectMapperApp.Mapper.Map<List<DocumentGroupDto>?>(list);

            // foreach (var documentGroupViewModel in list)
            // {

            //     var selectedGroupLanguageText = documentGroupViewModel.MultilanguageText
            //         .FirstOrDefault(t => t.Language == language);

            //     if (selectedGroupLanguageText != null)
            //     {
            //         documentGroupViewModel.Name = selectedGroupLanguageText.Label;
            //     }
            //     else if (documentGroupViewModel.MultilanguageText.Any())
            //     {
            //         documentGroupViewModel.Name = documentGroupViewModel.MultilanguageText.First().Label;
            //     }

            //     //TODO: Umut - Mapping
            //     // Check if DocumentDefinitionViewModels is null or empty before iterating
            //     // if (documentGroupViewModel.DocumentDefinitionList != null && documentGroupViewModel.DocumentDefinitionList.Any())
            //     // {
            //     //     // Apply the logic to each DocumentDefinitionViewModel in the DocumentGroupViewModel
            //     //     foreach (var documentDefinitionViewModel in documentGroupViewModel.DocumentDefinitionList)
            //     //     {
            //     //         var selectedLanguageText = documentDefinitionViewModel.MultilanguageText
            //     //             .FirstOrDefault(t => t.Language == language);

            //     //         if (selectedLanguageText != null)
            //     //         {
            //     //             documentDefinitionViewModel.Name = selectedLanguageText.Label;
            //     //         }
            //     //         else if (documentDefinitionViewModel.MultilanguageText.Any())
            //     //         {
            //     //             documentDefinitionViewModel.Name = documentDefinitionViewModel.MultilanguageText.First().Label;
            //     //         }
            //     //     }
            //     // }
            // }

            return Results.Ok(response);

        }
        catch (Exception ex)
        {
            Results.Problem(ex.Message);
        }
        return Results.NoContent();
    }

    private void MultiLanguageTextToNameByLanguage(string language)
    {

    }

}

