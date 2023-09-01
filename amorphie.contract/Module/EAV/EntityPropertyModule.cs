
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core;
using amorphie.contract.core.Entity.EAV;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class EntityPropertyModule
        : BaseBBTRoute<EntityProperty, EntityProperty, ProjectDbContext>
{
    public EntityPropertyModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Code" };

    public override string? UrlFragment => "entity-property";

}

