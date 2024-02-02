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
    async ValueTask<IResult> Instance([FromServices] IContractAppService contractAppService, CancellationToken token, [FromBody] ContractInstaceInputDto input, HttpContext httpContext)
    {
        var headerModels = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;

        input.EBankEntity = headerModels.EBankEntity;
        input.LangCode = headerModels.LangCode;
     
        var response = await contractAppService.Instance(input, token);
        if (response is null)
            return Results.NotFound();

        return Results.Ok(response);
    }

    public override string[]? PropertyCheckList => new string[] { "Code" };

    public override string? UrlFragment => "contract";

}