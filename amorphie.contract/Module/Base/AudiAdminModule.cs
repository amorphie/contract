
using amorphie.core.Module.minimal_api;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.Module.Base;


public abstract class AudiAdminModule<TDTOModel, TDBModel, TDbContext>
: BaseBBTRoute<TDTOModel, TDBModel, TDbContext> where TDTOModel : class, new() where TDBModel
: AudiEntity where TDbContext : DbContext
{
    protected AudiAdminModule(WebApplication app) : base(app)
    {
    }

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
    public override string? UrlFragment => "admin/";
}

