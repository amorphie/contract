
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class DocumentTemplateModule
        : BaseContractModule<DocumentTemplate, DocumentTemplate, DocumentTemplateValidator>
    {
        public DocumentTemplateModule(WebApplication app) : base(app)
        {
        }

        // public override string[]? PropertyCheckList => new string[] {"Code","Contact"};

        public override string? UrlFragment => "document-template";


     
    }

