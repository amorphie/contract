
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.EAV;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class EntityPropertyTypeModule
        : BaseContractModule<EntityPropertyType, EntityPropertyType, EntityPropertyTypeValidator>
    {
        public EntityPropertyTypeModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"Code"};

        public override string? UrlFragment => "entity-property-type";


   
       
    }

