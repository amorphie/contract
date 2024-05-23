using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
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
using amorphie.contract.infrastructure.Services.Kafka;
using amorphie.contract.core.Response;
using Microsoft.AspNetCore.Components.Forms;
using amorphie.contract.core.Extensions;
using amorphie.contract.application.Extensions;

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
        routeGroupBuilder.MapGet("getCategories", GetCategories);

    }

    async ValueTask<IResult> Instance([FromServices] IContractAppService contractAppService,
    CancellationToken token, [FromBody] ContractInstanceInputDto input, HttpContext httpContext)
    {
        // throw new ClientSideException("sdas", "InstanceState");

        var headerModels = HeaderHelper.GetHeader(httpContext);
        input.SetHeaderParameters(headerModels);
        var response = await contractAppService.Instance(input, token);

        return Results.Ok(response);
    }
    async ValueTask<IResult> InstanceState([FromServices] IContractAppService contractAppService, CancellationToken token,
    [AsParameters] ContractInstanceStateInputDto input, HttpContext httpContext)
    {

        var headerModels = HeaderHelper.GetHeader(httpContext);
        input.SetHeaderParameters(headerModels);

        var response = await contractAppService.InstanceState(input, token);
        return Results.Ok(response);
    }

    async ValueTask<IResult> GetCategories([FromServices] ProjectDbContext dbContext, [AsParameters] GetAllContractCategoryInputDto inputDto, HttpContext httpContext)
    {
        var lang = HeaderHelper.GetHeaderLangCode(httpContext);

        var query = dbContext.ContractCategory.AsQueryable();
        query = ContractHelperExtensions.LikeWhere(query, inputDto.Code);
        var list = query.Skip(inputDto.Page * inputDto.PageSize).Take(inputDto.PageSize).ToList();

        var response = list.Select(x => new
        {
            x.Id,
            x.Code,
            Title = x.Titles.L(lang),
        });

        return Results.Ok(response);
    }

    public override string[]? PropertyCheckList => new string[] { "Code" };

    public override string? UrlFragment => "contract";

}