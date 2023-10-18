
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.EAV;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class EntityPropertyTypeModule
        : BaseBBTRoute<EntityPropertyType, EntityPropertyType, ProjectDbContext>
    {
        public EntityPropertyTypeModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"Code"};

        public override string? UrlFragment => "entity-property-type";
 
}

