
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Mapping;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Model.Document;

namespace amorphie.contract;

public class DocumentModule
    : BaseBBTRoute<Document, Document, ProjectDbContext>
{
    public DocumentModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "DocumentContentId" };

    public override string? UrlFragment => "document";
    async ValueTask<IResult> getAllDocumentFullTextSearch(
           [FromServices] ProjectDbContext context, [AsParameters] PageComponentSearch dataSearch,
           CancellationToken cancellationToken
      )
    {
        var query = context!.Document
            .Skip(dataSearch.Page * dataSearch.PageSize)
            .Take(dataSearch.PageSize);


        if (!string.IsNullOrEmpty(dataSearch.Keyword))
        {
            query = query.Where(x => x.Status.Code == dataSearch.Keyword);
            // query = query.AsNoTracking().Where(p => p.SearchVector.Matches(EF.Functions.PlainToTsQuery("english", dataSearch.Keyword)));
        }

        var securityQuestions = await query.ToListAsync(cancellationToken);

        if (securityQuestions.Any())
        {
            var response = securityQuestions.Select(x => ObjectMapper.Mapper.Map<RootDocumentModel>(x));
            return Results.Ok(response);
        }

        return Results.NoContent();
    }
    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("search", getAllDocumentFullTextSearch);
    }

}






