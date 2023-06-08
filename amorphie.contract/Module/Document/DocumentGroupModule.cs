
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class DocumentGroupModule
        : BaseContractModule<DocumentGroup, DocumentGroup, DocumentGroupValidator>
    {
        public DocumentGroupModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"Code","Name","LanguageId"};

        public override string? UrlFragment => "document-group";

       
    }

