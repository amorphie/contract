
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class DocumentFormIODetailModule
        : BaseContractModule<DocumentFormIODetail, DocumentFormIODetail, DocumentFormIODetailValidator>
    {
        public DocumentFormIODetailModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"DocumentDefinitionId","DocumentFormIOId"};

        public override string? UrlFragment => "document-formio-detail";


     
    }

