
using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Document.DocumentGroups;

namespace amorphie.contract;

public class DocumentGroupDetailModule
    : BaseBBTRoute<DocumentGroupDetail, DocumentGroupDetail, ProjectDbContext>
{
    public DocumentGroupDetailModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] {"DocumentDefinitionId","DocumentGroupId"};

    public override string? UrlFragment => "document-group-detail";



}

