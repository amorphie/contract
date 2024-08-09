using System;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Extensions;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Response;
using amorphie.contract.Extensions;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.Module.Base;
using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;

namespace amorphie.contract.Module.Admin.Contract
{
	public class ContractCategoryModule : BaseBBTRoute<ContractCategory, ContractCategory, ProjectDbContext>
    {
        public ContractCategoryModule(WebApplication app) : base(app)
        {

        }

        public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
        {
            base.AddRoutes(routeGroupBuilder);
            routeGroupBuilder.MapGet("get-categories", GetCategories);
        }

        async ValueTask<IResult> GetCategories([FromServices] ProjectDbContext dbContext, [FromQuery] string? code, HttpContext httpContext)
        {
            var langCode = HeaderHelper.GetHeaderLangCode(httpContext);

            var query = dbContext.ContractCategory.AsQueryable();
            query = ContractHelperExtensions.LikeWhere(query, code);
            var list = query.ToList();

            var response = list.Select(x => new ContractCategoryResponseDto
            {
                Id = x.Id,
                Code = x.Code,
                Title = x.Titles.L(langCode),
                Contracts = x.ContractCategoryDetails.ToDictionary(d => d.ContractDefinition.Code, d => d.ContractDefinition.Titles.L(langCode))
            }).ToList();

            return Results.Ok(GenericResult<List<ContractCategoryResponseDto>>.Success(response));
        }

        public override string[]? PropertyCheckList => new string[] { "Code" };
        public override string? UrlFragment => "contract-category";
    }
}

