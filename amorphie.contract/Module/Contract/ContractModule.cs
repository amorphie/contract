using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.application.Extensions;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Response;
using amorphie.contract.Extensions;
using amorphie.contract.infrastructure.Contexts;
using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;

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
        var langCode = HeaderHelper.GetHeaderLangCode(httpContext);

        var query = dbContext.ContractCategory.AsQueryable();
        query = ContractHelperExtensions.LikeWhere(query, inputDto.Code);
        var list = query.Skip(inputDto.Page * inputDto.PageSize).Take(inputDto.PageSize).ToList();

        var response = list.Select(x => new ContractCategoryResponseDto
        {
            Id = x.Id,
            Code = x.Code,
            Title = x.Titles.L(langCode),
            CategoryContracts = x.ContractCategoryDetails.ToDictionary(d => d.ContractDefinition.Code, d => d.ContractDefinition.Titles.L(langCode))
        }).ToList();

        return Results.Ok(GenericResult<List<ContractCategoryResponseDto>>.Success(response));
    }

    public override string[]? PropertyCheckList => new string[] { "Code" };

    public override string? UrlFragment => "contract";

}