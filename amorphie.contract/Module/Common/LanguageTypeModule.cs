
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Common;

namespace amorphie.contract.common;

    public class LanguageTypeModule
        : BaseBBTRoute<LanguageType, LanguageType, ProjectDbContext>
    {
        public LanguageTypeModule(WebApplication app) : base(app)
        {
            
        }

        public override string[]? PropertyCheckList => new string[] {"Code"};

        public override string? UrlFragment => "Common-LanguageType";


   
       
    }

