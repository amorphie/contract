
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core;
using amorphie.contract.core.Entity.EAV;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.JsonPatch.Internal;
using AutoMapper;
using amorphie.core.Identity;

namespace amorphie.contract;

    public class EntityPropertyModule
        : BaseBBTRoute<EntityProperty, EntityProperty, ProjectDbContext>
    {
        public EntityPropertyModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"Code"};

        public override string? UrlFragment => "entity-property";

        protected override ValueTask<IResult> UpsertMethod([FromServices] IMapper mapper, [FromServices] IValidator<EntityProperty> validator, [FromServices] ProjectDbContext context, [FromServices] IBBTIdentity bbtIdentity, [FromBody] EntityProperty data, HttpContext httpContext, CancellationToken token)
        {
            return base.UpsertMethod(mapper, validator, context, bbtIdentity, data, httpContext, token);
        }

}

