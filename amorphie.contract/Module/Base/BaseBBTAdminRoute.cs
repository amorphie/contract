
using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
using FluentValidation;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using amorphie.contract.core.Entity.Base;
using Microsoft.AspNetCore.Routing;

namespace amorphie.contract;


public abstract class BaseBBTAdminRoute<TDTOModel, TDBModel, TDbContext>
: BaseBBTRoute<TDTOModel, TDBModel, TDbContext> where TDTOModel : class, new() where TDBModel
: AudiEntity where TDbContext : DbContext
{
    protected BaseBBTAdminRoute(WebApplication app) : base(app)
    {
    }

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        //routeGroupBuilder = routeGroupBuilder.MapGroup("ENEES");
        base.AddRoutes(routeGroupBuilder);
    }
    public override string? UrlFragment => "Admin/";
}

