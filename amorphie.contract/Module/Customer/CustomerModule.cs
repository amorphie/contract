using System.Collections.Generic;
using amorphie.contract.application;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Customer;
using amorphie.contract.application.Customer.Request;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;
using amorphie.contract.data.Contexts;
using amorphie.contract.data.Services;
using amorphie.core.Module.minimal_api;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.Module.Customer
{
    public class CustomerModule : BaseRoute
    {
        public CustomerModule(WebApplication app) : base(app)
        {
        }

        public override string? UrlFragment => "customer";

        public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapGet("get-documents-by-contracts", GetDocumentsByContracts);
            routeGroupBuilder.MapGet("get-documents-by-contracts2", GetDocumentsByContracts2);
            routeGroupBuilder.MapGet("get-all-documents", GetDocuments);
        }

        async ValueTask<IResult> GetDocumentsByContracts2([FromServices] ProjectDbContext context, [FromServices] ICustomerAppService customerAppService, HttpContext httpContext, CancellationToken token, [AsParameters] GetCustomerDocumentsByContractInputDto inputDto)
        {
            var headerModels = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;
            inputDto.SetHeaderParameters(headerModels.LangCode, headerModels.EBankEntity);

            var serviceResponse = await customerAppService.GetDocumentsByContracts2(inputDto, token);

            return Results.Ok(serviceResponse);
        }

        async ValueTask<IResult> GetDocumentsByContracts([FromServices] ProjectDbContext context, [FromServices] ICustomerAppService customerAppService, HttpContext httpContext, CancellationToken token, [AsParameters] GetCustomerDocumentsByContractInputDto inputDto)
        {
            var headerModels = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;
            inputDto.SetHeaderParameters(headerModels.LangCode, headerModels.EBankEntity);

            var serviceResponse = await customerAppService.GetDocumentsByContracts(inputDto, token);

            return Results.Ok(serviceResponse);
        }

        async ValueTask<IResult> GetDocuments([FromServices] ProjectDbContext context, [FromServices] ICustomerAppService customerAppService, HttpContext httpContext, CancellationToken token, [AsParameters] GetCustomerDocumentsByContractInputDto inputDto)
        {
            var response = await customerAppService.GetAllDocuments(inputDto, token);

            return Results.Ok(response);
        }
    }
}

