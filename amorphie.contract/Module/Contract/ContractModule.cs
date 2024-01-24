using System.Security.Cryptography.X509Certificates;

using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Mapping;
using AutoMapper;
using amorphie.contract.core.Model.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Diagnostics;
using amorphie.contract.core.Model.Contract;
using System.IO.Compression;
using System.Linq.Expressions;
using amorphie.contract.core.Enum;
using amorphie.contract.application;
using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Request;
using amorphie.core.IBase;
using Microsoft.AspNetCore.Mvc.Razor;
using amorphie.contract.application.Contract.Dto;

namespace amorphie.contract;

public class ContractModule
    : BaseBBTRoute<ContractDefinitionDto, Contract, ProjectDbContext>
{
    public ContractModule(WebApplication app) : base(app)
    {

    }
    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapPost("Instance", Instance);
    }
    async ValueTask<IResult> Instance([FromServices] IContractAppService contractAppService, CancellationToken token,
    [FromBody] ContractInstaceInputDto input, [FromHeader(Name = "Language")] string? language = "en-EN")
    {
        input.Lang = new LangDto
        {
            LangCode = language
        };

        var response = await contractAppService.Instance(input, token);
        if(response is null)
        return Results.NotFound();

        return Results.Ok(response);
    }

    public override string[]? PropertyCheckList => new string[] { "Code" };

    public override string? UrlFragment => "contract";

}