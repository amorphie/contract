
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Mapping;
using AutoMapper;

namespace amorphie.contract;

public class DocumentTypeModule : BaseBBTRoute<DocumentType, DocumentType, ProjectDbContext>
{
    public DocumentTypeModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Name", "ContentType" };

    public override string? UrlFragment => "document-type";
}

