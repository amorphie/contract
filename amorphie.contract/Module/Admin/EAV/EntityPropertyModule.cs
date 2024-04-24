using amorphie.contract.infrastructure.Contexts;
using FluentValidation;
using amorphie.contract.core.Entity.EAV;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using amorphie.core.Identity;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.EAV;

public class EntityPropertyModule
    : BaseAdminModule<EntityProperty, EntityProperty, ProjectDbContext>
{
    public EntityPropertyModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Code" };
    public override string? UrlFragment => base.UrlFragment + "entity-property";
    protected override ValueTask<IResult> UpsertMethod([FromServices] IMapper mapper, [FromServices] IValidator<EntityProperty> validator, [FromServices] ProjectDbContext context, [FromServices] IBBTIdentity bbtIdentity, [FromBody] EntityProperty data, HttpContext httpContext, CancellationToken token)
    {
        return base.UpsertMethod(mapper, validator, context, bbtIdentity, data, httpContext, token);
    }

}

