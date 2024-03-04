
using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class DocumentContentModule
        : BaseBBTRoute<DocumentContent, DocumentContent, ProjectDbContext>
    {
        public DocumentContentModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"ContentData","DocumentVersionsId"};

        public override string? UrlFragment => "document-content";


   
       
    }

