
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.EAV;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class EntityPropertyValueModule
        : BaseContractModule<EntityPropertyValue, EntityPropertyValue, EntityPropertyValueValidator>
    {
        public EntityPropertyValueModule(WebApplication app) : base(app)
        {
        }

        // public override string[]? PropertyCheckList => new string[] {"Code","Contact"};

        public override string? UrlFragment => "entity-property-value";


   
       
    }

