
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

public class DocumentDefinitionGroupDetailModule
    : BaseBBTRoute<DocumentDefinitionGroupDetail, DocumentDefinitionGroupDetail, ProjectDbContext>
{
    public DocumentDefinitionGroupDetailModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] {"DocumentDefinitionId","DocumentGroupId"};

    public override string? UrlFragment => "document-definition-group-detail";



}

