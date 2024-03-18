
using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Document.DocumentGroups;

namespace amorphie.contract;

public class DocumentGroupLanguageDetailModule
    : BaseBBTRoute<DocumentGroupLanguageDetail, DocumentGroupLanguageDetail, ProjectDbContext>
{
    public DocumentGroupLanguageDetailModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionGroupId", "MultiLanguageId" };

    public override string? UrlFragment => "document-group-language-detail";



}

