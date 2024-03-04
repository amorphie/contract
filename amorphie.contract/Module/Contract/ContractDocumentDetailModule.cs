
using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Contract;

namespace amorphie.contract;

    public class ContractDocumentDetailModule
        : BaseBBTRoute<ContractDocumentDetail, ContractDocumentDetail, ProjectDbContext>
    {
        public ContractDocumentDetailModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"ContractDefinitionId","DocumentDefinitionId"};

        public override string? UrlFragment => "contract-document-detail";


   
       
    }

