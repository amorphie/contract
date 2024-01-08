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
        // context.Contract.Add(data);
        // context.SaveChanges();
        ContractModel contractModel = new ContractModel();

        var query = context!.ContractDefinition.FirstOrDefault(x => x.Code == data.ContractName);
        if (query == null)
        {
            contractModel.Status = "not contract";
            return Results.Ok(contractModel);
        }
        contractModel.Status = EStatus.InProgress.ToString();
        contractModel.Id = query.Id;
        contractModel.Code = query.Code;

        var documentList = query.ContractDocumentDetails.
                            Select(x => x.DocumentDefinitionId)
                            .ToList();
        var documentGroupList = query.ContractDocumentGroupDetails.
                            SelectMany(x => x.DocumentGroup.DocumentGroupDetails).ToList().Select(a => a.DocumentDefinitionId).ToList();



        var customerDocument = context.Document.Where(x => x.Customer.Reference == data.Reference &&
                        documentList.Contains(x.DocumentDefinitionId)).ToList()
                        .Select(x => x.DocumentDefinitionId).ToList();

        var customerDocumentGroup = context.Document.Where(x => x.Customer.Reference == data.Reference &&
                                documentGroupList.Contains(x.DocumentDefinitionId)).ToList()
                                .Select(x => x.DocumentDefinitionId).ToList();

        var listDocument = query.ContractDocumentDetails.
        Where(d => !customerDocument.Contains(d.DocumentDefinitionId));

        var listDocumentGroup = query.ContractDocumentGroupDetails.ToList();
        // .SelectMany(x => x.DocumentGroup.DocumentGroupDetails).ToList()
        // .Where(x=>customerDocumentGroup.Contains( x.DocumentDefinitionId)).ToList();


        var listModel = listDocument.Select(x => new DocumentModel
        {
            Title = x.DocumentDefinition.DocumentDefinitionLanguageDetails
                .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
                .FirstOrDefault()?.MultiLanguage?.Name ?? x.DocumentDefinition.DocumentDefinitionLanguageDetails.FirstOrDefault().MultiLanguage.Name,

            Code = x.DocumentDefinition.Code,
            Status = EStatus.InProgress.ToString(),
            Required = x.Required,
            Render = x.DocumentDefinition.DocumentOnlineSing != null,
            Version = x.DocumentDefinition.Semver,
            OnlineSign = new OnlineSignModel
            {
                DocumentModelTemplate = x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == language).Count() > 0 ?
                x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == language).Select(b => new DocumentModelTemplate
                {
                    Name = b.DocumentTemplate.Code,
                    MinVersion = b.DocumentTemplate.Version,
                }).ToList() : x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Select(b => new DocumentModelTemplate
                {
                    Name = b.DocumentTemplate.Code,
                    MinVersion = b.DocumentTemplate.Version,
                }).Take(1).ToList(),

                ScaRequired = x.DocumentDefinition.DocumentOnlineSing != null ? x.DocumentDefinition.DocumentOnlineSing.Required : false,
                AllovedClients = x.DocumentDefinition.DocumentOnlineSing.DocumentAllowedClientDetails
                                            .Select(x => x.DocumentAllowedClients.Code)
                                            .ToList() ?? new List<string>()
            }
        }
        ).ToList();

        var listModelGroup = listDocumentGroup.Select(a =>
        new DocumentGroupModel
        {
            Title = a.DocumentGroup.DocumentGroupLanguageDetail
                .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
                .FirstOrDefault()?.MultiLanguage?.Name ?? a.DocumentGroup.DocumentGroupLanguageDetail.FirstOrDefault().MultiLanguage.Name,
            Status = a.AtLeastRequiredDocument >= a.DocumentGroup.DocumentGroupDetails
            .Where(c => !customerDocumentGroup.Contains(c.DocumentDefinitionId)).Count() ? EStatus.Completed.ToString() : EStatus.InProgress.ToString(),

            AtLeastRequiredDocument = a.AtLeastRequiredDocument,

            Required = a.Required,
            Document = a.DocumentGroup.DocumentGroupDetails
            .Where(c => !customerDocumentGroup.Contains(c.DocumentDefinitionId)).Select(x => new DocumentModel
            {
                Title = x.DocumentDefinition.DocumentDefinitionLanguageDetails
               .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
               .FirstOrDefault()?.MultiLanguage?.Name ?? x.DocumentDefinition.DocumentDefinitionLanguageDetails.FirstOrDefault().MultiLanguage.Name,
                Code = x.DocumentDefinition.Code,
                Status = EStatus.InProgress.ToString(),
                Render = x.DocumentDefinition.DocumentOnlineSing != null,
                Version = x.DocumentDefinition.Semver,
                OnlineSign = new OnlineSignModel
                {
                    DocumentModelTemplate = x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == language).Count() > 0 ?
               x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == language).Select(b => new DocumentModelTemplate
               {
                   Name = b.DocumentTemplate.Code,
                   MinVersion = b.DocumentTemplate.Version,
               }).ToList() : x.DocumentDefinition.DocumentOnlineSing.DocumentTemplateDetails.Select(b => new DocumentModelTemplate
               {
                   Name = b.DocumentTemplate.Code,
                   MinVersion = b.DocumentTemplate.Version,
               }).Take(1).ToList(),

                    ScaRequired = x.DocumentDefinition.DocumentOnlineSing != null ? x.DocumentDefinition.DocumentOnlineSing.Required : false,
                    AllovedClients = x.DocumentDefinition.DocumentOnlineSing.DocumentAllowedClientDetails
                                           .Select(x => x.DocumentAllowedClients.Code)
                                           .ToList() ?? new List<string>()
                }
            }

             ).ToList()
        }

        ).ToList();
        contractModel.Document = listModel;

        contractModel.DocumentGroups = listModelGroup;

        if (contractModel.Document.Count == 0)
        {
            contractModel.Status = EStatus.Completed.ToString();
        }


        return Results.Ok(contractModel);

        //         var documentdeflist2 = context.DocumentDefinition.ToList().
        //     Where(x => query.ContractDocumentDetails.Any(a => a.DocumentDefinitionCode == x.Code))
        //     .ToList();
        //         var documentModels2 = documentdeflist2
        //             .Join(
        //                 list,
        //                 document => new { Code = document.Code },
        //                 contractDetail => new { Code = contractDetail.DocumentDefinitionCode },
        //                 (document, contractDetail) => new DocumentModel
        //                 {
        //                     Title = document.DocumentDefinitionLanguageDetails
        //                 .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
        //                 .FirstOrDefault()?.MultiLanguage?.Name ?? document.DocumentDefinitionLanguageDetails.FirstOrDefault().MultiLanguage.Name,

        //                     Code = document.Code,
        //                     Status = "not-started",
        //                     Required = contractDetail.Required,
        //                     Render = document.DocumentOnlineSing != null,
        //                     Version = document.Semver,
        //                     OnlineSign = new OnlineSignModel
        //                     {
        //                         DocumentModelTemplate = document.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == language).Count() > 0 ? document.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == language).Select(b => new DocumentModelTemplate
        //                         {
        //                             Name = b.DocumentTemplate.Code,
        //                             MinVersion = b.DocumentTemplate.Version,
        //                         }).ToList() : document.DocumentOnlineSing.DocumentTemplateDetails.Select(b => new DocumentModelTemplate
        //                         {
        //                             Name = b.DocumentTemplate.Code,
        //                             MinVersion = b.DocumentTemplate.Version,
        //                         }).Take(1).ToList(),

        //                         ScaRequired = document.DocumentOnlineSing != null ? document.DocumentOnlineSing.Required : false,
        //                         AllovedClients = document.DocumentOnlineSing.DocumentAllowedClientDetails
        //                                             .Select(x => x.DocumentAllowedClients.Code)
        //                                             .ToList() ?? new List<string>()
        //                     }
        //                 }
        //             ).ToList();
        //         contractModel.Document = documentModels2;
        //         if (documentModels2.Count == 0)
        //         {
        //             contractModel.Status = "valid";
        //         }
        //         return Results.Ok(contractModel);
        //     }


        //     var documentdeflist = context.DocumentDefinition
        // .Where(x => query.ContractDocumentDetails
        //     .Any(a => a.DocumentDefinitionCode == x.Code && a.DocumentDefinitionSemver == x.Semver))
        // .ToList();

        //     var documentModels = documentdeflist
        //         .Join(
        //             query.ContractDocumentDetails,
        //             document => new { Code = document.Code },
        //             contractDetail => new { Code = contractDetail.DocumentDefinitionCode },
        //             (document, contractDetail) => new DocumentModel
        //             {
        //                 Title = document.DocumentDefinitionLanguageDetails
        //             .Where(dl => dl.MultiLanguage.LanguageType.Code == language)
        //             .FirstOrDefault()?.MultiLanguage?.Name ?? document.DocumentDefinitionLanguageDetails.FirstOrDefault().MultiLanguage.Name,

        //                 Code = document.Code,
        //                 Status = "not-started",
        //                 Required = contractDetail.Required,
        //                 Render = document.DocumentOnlineSing != null,
        //                 Version = document.Semver,
        //                 OnlineSign = new OnlineSignModel
        //                 {
        //                     DocumentModelTemplate = document.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == language).Count() > 0 ? document.DocumentOnlineSing.DocumentTemplateDetails.Where(x => x.DocumentTemplate.LanguageType.Code == language).Select(b => new DocumentModelTemplate
        //                     {
        //                         Name = b.DocumentTemplate.Code,
        //                         MinVersion = b.DocumentTemplate.Version,
        //                     }).ToList() : document.DocumentOnlineSing.DocumentTemplateDetails.Select(b => new DocumentModelTemplate
        //                     {
        //                         Name = b.DocumentTemplate.Code,
        //                         MinVersion = b.DocumentTemplate.Version,
        //                     }).Take(1).ToList(),

        //                     ScaRequired = document.DocumentOnlineSing != null ? document.DocumentOnlineSing.Required : false,
        //                     AllovedClients = document.DocumentOnlineSing.DocumentAllowedClientDetails
        //                                         .Select(x => x.DocumentAllowedClients.Code)
        //                                         .ToList() ?? new List<string>()
        //                 }
        //             }
        //         ).ToList();

    }
    public override string[]? PropertyCheckList => new string[] { "Code" };

    public override string? UrlFragment => "contract";

}