
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Contract;

namespace amorphie.contract;

    public class ContractDefinitionModule
        : BaseContractModule<ContractDefinition, ContractDefinition, ContractDefinitionValidator>
    {
        public ContractDefinitionModule(WebApplication app) : base(app)
        {
        }

        public override string[]? PropertyCheckList => new string[] {"Name","Code"};

        public override string? UrlFragment => "contract-definition";


   
       
    }

