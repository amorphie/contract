
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class DocumentAllowedModule
        : BaseBBTRoute<DocumentAllowed, DocumentAllowed, ProjectDbContext>
{
    public DocumentAllowedModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Name" };

    public override string? UrlFragment => "document-allowed";


}

