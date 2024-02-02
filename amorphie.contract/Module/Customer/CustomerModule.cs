using System.Collections.Generic;
using amorphie.contract.application;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Customer;
using amorphie.contract.application.Customer.Request;
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
            routeGroupBuilder.MapGet("get-all-documents", GetDocuments);
        }

        async ValueTask<IResult> GetDocumentsByContracts([FromServices] ProjectDbContext context, [FromServices] ICustomerAppService customerAppService, HttpContext httpContext, CancellationToken token, [AsParameters] GetCustomerDocumentsByContractInputDto inputDto)
        {
            var serviceResponse = await customerAppService.GetDocumentsByContracts(inputDto, token);

            return Results.Ok(serviceResponse);

            //    var contracts = context!.ContractDefinition.ToList();

            //    var documents = await context.Document.Where(x => x.Customer.Reference == reference).ToListAsync(token);

            //    List<ContractDefinitionDto> contractModels = contracts.Select(x => new ContractDefinitionDto
            //    {
            //        Id = x.Id,
            //        Code = x.Code,
            //        Status = "inProgress"
            //    }).ToList();

            //    foreach (var model in contractModels)
            //    {
            //        var currentContractDocumentCodes = contracts.Where(y => y.Id == model.Id).FirstOrDefault().ContractDocumentDetails.
            //                    Select(x => new { x.DocumentDefinitionId, x.Required })
            //                    .ToList();

            //        var currentContractDocuments = documents.Where(x => currentContractDocumentCodes.Exists(y => x.DocumentDefinitionId == y.DocumentDefinitionId)).ToList();

            //        model.ContractDocumentDetails = currentContractDocuments.Select(x => new DocumentModel
            //        {
            //            Title = x.DocumentDefinition.DocumentDefinitionLanguageDetails
            //            .Where(dl => dl.MultiLanguage.LanguageType.Code == "en-EN")
            //            .FirstOrDefault()?.MultiLanguage?.Name ?? x.DocumentDefinition.DocumentDefinitionLanguageDetails.FirstOrDefault().MultiLanguage.Name,

            //            Code = x.DocumentDefinition.Code,
            //            Status = "not-started",
            //            Required = currentContractDocumentCodes.Where(y => y.DocumentDefinitionId == x.DocumentDefinitionId).FirstOrDefault().Required,
            //            Render = x.DocumentDefinition.DocumentOnlineSing != null,
            //            Version = x.DocumentDefinition.Semver,

            //            OnlineSign = new OnlineSignDto
            //            {
            //                DocumentModelTemplate = x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == "en-EN").Count() > 0 ? x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == "en-EN").Select(b => new DocumentModelTemplate
            //                {
            //                    Name = b.DocumentTemplate.Code,
            //                    MinVersion = b.DocumentTemplate.Version,
            //                }).ToList() : x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Select(b => new DocumentModelTemplate
            //                {
            //                    Name = b.DocumentTemplate.Code,
            //                    MinVersion = b.DocumentTemplate.Version,
            //                }).Take(1).ToList(),

            //                ScaRequired = x.DocumentDefinition.DocumentOnlineSing != null ? x.DocumentDefinition.DocumentOnlineSing.Required : false,
            //                AllovedClients = x.DocumentDefinition.DocumentOnlineSing.DocumentAllowedClientDetails
            //                                        .Select(x => x.DocumentAllowedClients.Code)
            //                                        .ToList() ?? new List<string>()
            //            }
            //        }).ToList();
            //    }

            //    return Results.Ok(contractModels);
        }

        async ValueTask<IResult> GetDocuments([FromServices] ProjectDbContext context, [FromServices] IMapper mapper,
                    HttpContext httpContext, CancellationToken token, string reference)
            {
                IDocumentService documentService = new DocumentService();
                var documents = await context.Document.Where(x => x.Customer.Reference == reference)
                .Select(x => new
                {
                    x.DocumentDefinition.Code,
                    x.DocumentDefinition.Semver,
                    status = "valid",
                    MinioUrl = documentService.GetDocumentPath(x.DocumentContent.MinioObjectName, token),
                    x.DocumentContent.MinioObjectName,
                    x.Customer.Reference
                }).ToListAsync(token);

                return Results.Ok(documents);
        }
    }
}

