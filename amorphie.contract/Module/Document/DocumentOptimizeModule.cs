
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class DocumentOptimizeModule
        : BaseBBTRoute<DocumentOptimize, DocumentOptimize, ProjectDbContext>
    {
        public DocumentOptimizeModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"Size","Transform"};

        public override string? UrlFragment => "document-optimize";

       
    }

