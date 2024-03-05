
using amorphie.core.Module.minimal_api;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.infrastructure.Contexts;

namespace amorphie.contract;

    public class DocumentInstanceEntityPropertyModule
        : BaseBBTRoute<DocumentInstanceEntityProperty, DocumentInstanceEntityProperty, ProjectDbContext>
    {
        public DocumentInstanceEntityPropertyModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"DocumentId","EntityPropertyId"};

        public override string? UrlFragment => "document-instance-entity-property";


      
       
    }

