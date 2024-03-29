
using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.EAV;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class EntityPropertyValueModule :
     BaseBBTRoute<EntityPropertyValue, EntityPropertyValue, ProjectDbContext>
{
    public EntityPropertyValueModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Data" };

    public override string? UrlFragment => "entity-property-value";

}

