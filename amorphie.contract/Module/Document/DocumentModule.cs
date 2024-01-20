using System.Xml.XPath;

using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Mapping;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Model.Document;
using AutoMapper;
using amorphie.contract.core.Model;
using amorphie.contract.core.Services;
using amorphie.contract.core.Entity;
using System.Buffers.Text;
using amorphie.contract.core.Enum;
using amorphie.contract.application;

namespace amorphie.contract;

public class DocumentModule
    : BaseBBTRoute<Document, Document, ProjectDbContext>
{
    public DocumentModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "DocumentContentId" };

    public override string? UrlFragment => "document";
    async ValueTask<IResult> getAllDocumentFullTextSearch(
           [FromServices] IDocumentAppService documentAppService, [AsParameters] PageComponentSearch dataSearch,
           CancellationToken cancellationToken)
    {
        var inputDto = new GetAllDocumentInputDto
        {
            Keyword = dataSearch.Keyword,
            Page = dataSearch.Page,
            PageSize = dataSearch.PageSize
        };

        var response = await documentAppService.GetAllDocumentFullTextSearch(inputDto, cancellationToken);

        if (response == null || response.Count == 0)
            return Results.NoContent();

        return Results.Ok(response);
    }

    async ValueTask<IResult> getAllDocumentAll(
          [FromServices] ProjectDbContext context,
          CancellationToken cancellationToken
     )

    {
        try
        {


            var query = context!.Document;

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {
                // var response = securityQuestions.Select(x => ObjectMapper.Mapper.Map<RootDocumentModel>(x));
                var response = securityQuestions.Select(x =>
                 new RootDocumentModel
                 {
                     Id = x.Id.ToString(),
                     DocumentDefinitionId = x.DocumentDefinitionId.ToString(),
                     StatuCode = x.Status.ToString(),
                     CreatedAt = x.CreatedAt,
                     DocumentDefinition = new DocumentDefinitionModel
                     {
                         Code = x.DocumentDefinition.Code,
                         MultilanguageText = x.DocumentDefinition.DocumentDefinitionLanguageDetails!
                                .Select(a => new MultilanguageText
                                {
                                    Label = a.MultiLanguage.Name,
                                    Language = a.MultiLanguage.LanguageType.Code
                                }).ToList(),
                         DocumentOperations = new DocumentOperationsModel
                         {
                             DocumentManuelControl = x.DocumentDefinition.DocumentOperations!.DocumentManuelControl,
                             DocumentOperationsTagsDetail = x.DocumentDefinition.DocumentOperations.DocumentOperationsTagsDetail!.Select(x => new TagModel
                             {
                                 Contact = x.Tags.Contact,
                                 Code = x.Tags.Code
                             }).ToList()
                         }
                     },
                     DocumentContent = new DocumentContentModel
                     {
                         ContentData = x.DocumentContent.ContentData,
                         KiloBytesSize = x.DocumentContent.KiloBytesSize,
                         ContentType = x.DocumentContent.ContentType,
                         ContentTransferEncoding = x.DocumentContent.ContentTransferEncoding,
                         Name = x.DocumentContent.Name,
                         Id = x.DocumentContent.Id.ToString()

                     }
                 }).ToList();


                return Results.Ok(response);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return Results.NoContent();
    }
    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("search", getAllDocumentFullTextSearch);
        routeGroupBuilder.MapGet("getAll", getAllDocumentAll);
        routeGroupBuilder.MapPost("Instance", Instance);
    }

    async ValueTask<IResult> Instance([FromServices] ProjectDbContext context, [FromServices] IMapper mapper,
                    HttpContext httpContext, CancellationToken token,
                    // IFormFile file,
                    //  [AsParameters] ComponentSearch data,
                    [FromBody] DocumentInstanceModel data, [FromServices] IMinioService minioService)
    {
        var docdef = context.DocumentDefinition.FirstOrDefault(x => x.Code == data.DocumentCode && x.Semver == data.DocumentVersion);
        if (docdef == null)
        {
            return Results.NotFound("Document Code ve versiyona ait kayit bulunamadi!");
        }

        // if (!(file == null && file.Length == 0))
        // {
        //     using (var stream = new MemoryStream())
        //     {
        //         await file.CopyToAsync(stream);
        //         data.fileByteArray = stream.ToArray();
        //     }
        // }
        var cus = context.Customer.FirstOrDefault(x => x.Reference == data.Reference);
        if (cus == null)
        {
            cus = new Customer
            {
                Reference = data.Reference,
                Owner = data.Owner
            };
            context.Customer.Add(cus);

        }

        var document = new Document
        {
            Id = data.Id,
            DocumentDefinitionId = docdef.Id,
            Status = EStatus.Completed,
            CustomerId = cus.Id,
            DocumentContent = new DocumentContent
            {
                ContentData = data.FileContext.ToString(),
                KiloBytesSize = data.FileContext.ToString().Length.ToString(),
                ContentType = data.FileType,
                MinioObjectName = data.ToString(),
                ContentTransferEncoding = data.FileType,
                Name = data.ToString(),
            }
        };
        context.Document.Add(document);
        context.SaveChanges();

        byte[] fileByteArray;
        if (data.FileContextType == "byte")
        {
            fileByteArray = data.FileContext.Split(',').Select(byte.Parse).ToArray();
        }
        else
        {
            fileByteArray = Convert.FromBase64String(data.FileContext);
        }

        await minioService.UploadFile(fileByteArray, data.ToString(), data.FileType, "");

        return Results.Ok();
    }
    protected override async ValueTask<IResult> GetAllMethod([FromServices] ProjectDbContext context, [FromServices] IMapper mapper,
    [FromQuery][Range(0, 100)] int page, [FromQuery][Range(5, 100)] int pageSize, HttpContext httpContext, CancellationToken token)
    {
        var query = context!.Document;

        var securityQuestions = await query.ToListAsync(token);

        if (securityQuestions.Any())
        {

            // var response = securityQuestions.Select(x => ObjectMapper.Mapper.Map<RootDocumentModel>(x));
            var response = securityQuestions.Select(x =>
             new RootDocumentModel
             {
                 Id = x.Id.ToString(),
                 DocumentDefinitionId = x.DocumentDefinitionId.ToString(),
                 StatuCode = x.Status.ToString(),
                 CreatedAt = x.CreatedAt,
                 DocumentDefinition = new DocumentDefinitionModel
                 {
                     Code = x.DocumentDefinition.Code,
                     MultilanguageText = x.DocumentDefinition.DocumentDefinitionLanguageDetails!
                            .Select(a => new MultilanguageText
                            {
                                Label = a.MultiLanguage.Name,
                                Language = a.MultiLanguage.LanguageType.Code
                            }).ToList(),
                     DocumentOperations = new DocumentOperationsModel
                     {
                         DocumentManuelControl = x.DocumentDefinition.DocumentOperations!.DocumentManuelControl,
                         DocumentOperationsTagsDetail = x.DocumentDefinition.DocumentOperations.DocumentOperationsTagsDetail!.Select(x => new TagModel
                         {
                             Contact = x.Tags.Contact,
                             Code = x.Tags.Code
                         }).ToList()
                     }
                 },
                 DocumentContent = new DocumentContentModel
                 {
                     ContentData = x.DocumentContent.ContentData,
                     KiloBytesSize = x.DocumentContent.KiloBytesSize,
                     ContentType = x.DocumentContent.ContentType,
                     ContentTransferEncoding = x.DocumentContent.ContentTransferEncoding,
                     Name = x.DocumentContent.Name,
                     Id = x.DocumentContent.Id.ToString()

                 }
             }).ToList();
            return Results.Ok(response);
        }
        return Results.NoContent();
    }
}






