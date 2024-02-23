using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using Microsoft.AspNetCore.Mvc;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.application;
using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.Extensions;
using amorphie.contract.core.Model;
using amorphie.contract.core.Enum;
using amorphie.core.Base;
using Microsoft.VisualBasic;

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
        routeGroupBuilder.MapGet("InstanceState", InstanceState);

    }

    async ValueTask<IResult> Instance([FromServices] IContractAppService contractAppService,
    CancellationToken token, [FromBody] ContractInstanceInputDto input, HttpContext httpContext)
    {
        var headerModels = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;
        input.SetHeaderParameters(headerModels);
        var response = await contractAppService.Instance(input, token);

        return Results.Ok(new
        {
            Data = response,
            Success = true,
            ErrorMessage = "",
        });
    }
    async ValueTask<IResult> InstanceState([FromServices] IContractAppService contractAppService, CancellationToken token,
    [AsParameters] ContractInstanceInputDto input, HttpContext httpContext)
    {
        //
        var headerModels = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;
        var inputQ = new ContractInstanceInputDto
        {
            ContractName = input.ContractName,
        };
        inputQ.SetHeaderParameters(headerModels);

        var response = await contractAppService.InstanceState(inputQ, token);
        return Results.Ok(new
        {
            Data = response,
            Success = true,
            ErrorMessage = "",
        });
    }

    public override string[]? PropertyCheckList => new string[] { "Code" };

    public override string? UrlFragment => "contract";

}