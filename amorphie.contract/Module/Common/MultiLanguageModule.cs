
using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Common;

namespace amorphie.contract.common;

    public class MultiLanguageModule
        : BaseBBTRoute<MultiLanguage, MultiLanguage, ProjectDbContext>
{
    public MultiLanguageModule(WebApplication app) : base(app)
    {

    }

    public override string[]? PropertyCheckList => new string[] { "Name" };

    public override string? UrlFragment => "Common-MultiLanguage";




}

