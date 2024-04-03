
using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
using FluentValidation;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using amorphie.contract.core.Entity.Base;
using Microsoft.AspNetCore.Routing;

namespace amorphie.contract.Module.Base;


public abstract class BaseAdminModule<TDTOModel, TDBModel, TDbContext>
: BaseBBTRoute<TDTOModel, TDBModel, TDbContext> where TDTOModel : class, new() where TDBModel
: BaseEntity where TDbContext : DbContext
{
    protected BaseAdminModule(WebApplication app) : base(app)
    {
    }

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);

        this.GetByCode(routeGroupBuilder);
        this.DeleteByCode(routeGroupBuilder);
        this.DoesExist(routeGroupBuilder);

    }
    protected virtual void GetByCode(RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapGet("/by-code/{code}", new Func<TDbContext, IMapper, string, HttpContext, CancellationToken, ValueTask<IResult>>(GetMethodByCode)).Produces<TDTOModel>(200, null, Array.Empty<string>()).Produces(404, null, null);
    }
    protected virtual void DoesExist(RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapGet("/does-exist/{keyword}", new Func<TDbContext, IMapper, string, HttpContext, CancellationToken, ValueTask<bool>>(GetMethodDoesExist)).Produces<TDTOModel>(200, null, Array.Empty<string>()).Produces(404, null, null);
    }
    protected virtual async ValueTask<IResult> GetMethodByCode([FromServices] TDbContext context, [FromServices] IMapper mapper,
     [FromRoute] string code, HttpContext httpContext, CancellationToken token)
    {
        DbSet<TDBModel> source = context.Set<TDBModel>();
        TDBModel val = await source.AsNoTracking().FirstOrDefaultAsync((TDBModel x) => x.Code == code, token);
        IResult result2;
        if (val == null)
        {
            IResult result = TypedResults.NotFound();
            result2 = result;
        }
        else
        {
            IResult result = TypedResults.Ok(mapper.Map<TDTOModel>(val));
            result2 = result;
        }
        return result2;
    }

    protected virtual async ValueTask<bool> GetMethodDoesExist([FromServices] TDbContext context, [FromServices] IMapper mapper,
     [FromRoute] string keyword, HttpContext httpContext, CancellationToken token)
    {
        DbSet<TDBModel> source = context.Set<TDBModel>();

        if (!string.IsNullOrEmpty(keyword) && keyword != "*")
        {
            if (keyword.Contains("%"))
            {
                string pattern = keyword.Replace("%", "");
                return await source.AsNoTracking().AnyAsync(x => x.Code.StartsWith(pattern) || x.Code.EndsWith(pattern), token);
            }
            else
            {
                return await source.AsNoTracking().AnyAsync(x => x.Code == keyword, token);
            }
        }

        return false;
    }

    protected virtual void DeleteByCode(RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapDelete("/by-code/{code}", new Func<IMapper, TDbContext, string, HttpContext, CancellationToken, ValueTask<IResult>>(DeleteMethodByCode)).Produces<TDTOModel>(200, null, Array.Empty<string>()).Produces(404, null, null);
    }

    protected virtual async ValueTask<IResult> DeleteMethodByCode([FromServices] IMapper mapper, [FromServices] TDbContext context, [FromRoute(Name = "code")] string code, HttpContext httpContext, CancellationToken token)
    {
        DbSet<TDBModel> dbSet = context.Set<TDBModel>();
        TDBModel model = await dbSet.AsNoTracking().FirstOrDefaultAsync((TDBModel x) => x.Code == code, token);
        if (model != null)
        {
            dbSet.Remove(model);
            await context.SaveChangesAsync(token);
            return Results.Ok(mapper.Map<TDTOModel>(model));
        }

        return Results.NotFound();
    }
    public override string? UrlFragment => "admin/";
}

