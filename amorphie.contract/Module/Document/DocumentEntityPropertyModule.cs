
using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class DocumentEntityPropertyModule
        : BaseBBTRoute<DocumentEntityProperty, DocumentEntityProperty, ProjectDbContext>
    {
        public DocumentEntityPropertyModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"DocumentDefinitionId","EntityPropertyId"};

        public override string? UrlFragment => "document-entity-property";


      
       
    }

