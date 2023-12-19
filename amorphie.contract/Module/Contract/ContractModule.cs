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

namespace amorphie.contract;

public class ContractModule
    : BaseBBTRoute<Contract, Contract, ProjectDbContext>
{
    public ContractModule(WebApplication app) : base(app)
    {

    }
    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapPost("Instance", Instance);
    }
    async ValueTask<IResult> Instance([FromServices] ProjectDbContext context, [FromServices] IMapper mapper,
   HttpContext httpContext, CancellationToken token,
    //  [AsParameters] ComponentSearch data,
    [FromBody] Contract data,
  [FromHeader(Name = "Language")] string? language = "en-EN")
    {
        var query = context!.ContractDefinition.FirstOrDefault(x => x.Code == data.ContractName);
        if (query == null)
        {
            return Results.Ok(new { status = "not contract" });
        }
        var documentList = query.ContractDocumentDetails.
                            Select(x => new { x.DocumentDefinitionCode, x.Semver })
                            .ToList();

        var oldCreateDocument = context.Document?.Any(x => x.Customer.Reference == data.Reference);

        if (oldCreateDocument.Value && context.Document != null)
        {
            var customerDocument = context.Document.Where(x => x.Customer.Reference == data.Reference &&
                 documentList.Any(d => d.DocumentDefinitionCode == x.DocumentDefinition.Code && d.Semver == x.DocumentDefinition.Semver)).Select(x => new { x.DocumentDefinition.Code, x.DocumentDefinition.Semver }).ToList();

            var list = query.ContractDocumentDetails.Where(d => !customerDocument.Any(cd => cd.Code == d.DocumentDefinitionCode && cd.Semver == d.Semver)).ToList();
            return Results.Ok(list);
        }
        ContractModel contractModel = new ContractModel();
        contractModel.id = query.Id;
        contractModel.status = "in-progress";

        var documentdeflist = context.DocumentDefinition.ToList().
        Where(x => query.ContractDocumentDetails.Any(a => a.DocumentDefinitionCode == x.Code))
        .ToList();
        var documentModels = documentdeflist
            .Join(
                query.ContractDocumentDetails,
                document => new { Code = document.Code },
                contractDetail => new { Code = contractDetail.DocumentDefinitionCode },
                (document, contractDetail) => new DocumentModel
                {
                    title = document.DocumentDefinitionLanguageDetails
                .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
                .FirstOrDefault()?.MultiLanguage?.Name ?? document.DocumentDefinitionLanguageDetails.FirstOrDefault().MultiLanguage.Name,

                    name = document.Code,
                    status = "not-started",
                    required = contractDetail.Required,
                    render = document.DocumentOnlineSing != null,
                    online_sign = new OnlineSignModel
                    {
                        version = document.Semver,
                        sca_required = document.DocumentOnlineSing != null ? document.DocumentOnlineSing.Required : false,
                        alloved_clients = document.DocumentOnlineSing.DocumentAllowedClientDetails
                                            .Select(x => x.DocumentAllowedClients.Code)
                                            .ToList() ?? new List<string>()
                    }
                }
            ).ToList();

        contractModel.document = documentModels;


        return Results.Ok(contractModel);
    }
    public override string[]? PropertyCheckList => new string[] { "Code" };

    public override string? UrlFragment => "contract";

}

