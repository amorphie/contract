
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Document.DocumentTypes;

namespace amorphie.contract.Module.Document.DocumentTypes;

    public class DocumentOnlineSingModule
        : BaseBBTRoute<DocumentOnlineSing, DocumentOnlineSing, ProjectDbContext>
    {
        public DocumentOnlineSingModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"Required"};

        public override string? UrlFragment => "document-online-sing";


   
       
    }

