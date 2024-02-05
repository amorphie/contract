
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Common;

namespace amorphie.contract.common;

public class TagModule
    : BaseBBTContractRoute<Tag, Tag, ProjectDbContext>
{
    public TagModule(WebApplication app) : base(app)
    {

    }

    public override string[]? PropertyCheckList => new string[] { "Name" };

    public override string? UrlFragment => "Common-Tag";


    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("getallHeaders", getallHeaders);
    }

    async ValueTask<IResult> getallHeaders(HttpContext httpContext)
    {
        Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
        foreach (var header in httpContext.Request.Headers)
        {
            requestHeaders.Add(header.Key, header.Value);
        }

        return Results.Ok(requestHeaders);
    }


}

