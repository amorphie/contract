
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Mapping;


namespace amorphie.contract;

public class DocumentFormatTypeModule : BaseBBTRoute<DocumentFormatType, DocumentFormatType , ProjectDbContext>
{
    public DocumentFormatTypeModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Name", "ContentType" };

    public override string? UrlFragment => "document-format-type";
}

