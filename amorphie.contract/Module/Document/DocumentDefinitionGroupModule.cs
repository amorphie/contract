
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class DocumentDefinitionGroupModule
        : BaseBBTRoute<DocumentDefinitionGroup, DocumentDefinitionGroup, ProjectDbContext>
{
    public DocumentDefinitionGroupModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Code", "Name" };

    public override string? UrlFragment => "document-definition-group";


}

