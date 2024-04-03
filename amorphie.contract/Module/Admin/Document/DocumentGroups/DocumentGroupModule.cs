using amorphie.contract.infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.application;
using amorphie.core.Extension;
using amorphie.contract.application.Extensions;
using amorphie.contract.core.Extensions;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document.DocumentGroups;

public class DocumentGroupModule
    : BaseAdminModule<DocumentGroup, DocumentGroup, ProjectDbContext>
{
    public DocumentGroupModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Code", "Name" };
    public override string? UrlFragment => base.UrlFragment +"document-group";
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
            title = x.Titles.L(language)

        }).ToListAsync(token);
        // var list = await query.ToListAsync(token);
        return Results.Ok(list);
    }
    async ValueTask<IResult> getAnyDocumentGroupListSearch(
            [FromServices] ProjectDbContext context, [AsParameters] ComponentSearch dataSearch,
            CancellationToken cancellationToken
        )
    {
            var query = context!.DocumentGroup.AsQueryable();
            var anyValue = false;
            if (!string.IsNullOrEmpty(dataSearch.Keyword))
            {
                anyValue = query.Any(x => dataSearch.Keyword == x.Code);
            }
            return Results.Ok(anyValue);
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

            var response = ObjectMapperApp.Mapper.Map<List<DocumentGroupDto>?>(list, opt => opt.Items[Lang.LangCode] = language);
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            Results.Problem(ex.Message);
        }
        return Results.NoContent();
    }
}

