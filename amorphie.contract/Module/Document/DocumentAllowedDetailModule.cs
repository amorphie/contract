
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class DocumentAllowedDetailModule
        : BaseContractModule<DocumentAllowedDetail, DocumentAllowedDetail, DocumentAllowedDetailValidator>
    {
        public DocumentAllowedDetailModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"DocumentDefinitionId","DocumentAllowedId"};

        public override string? UrlFragment => "document-allowed-detail";


    }
