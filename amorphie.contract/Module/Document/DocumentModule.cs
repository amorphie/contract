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
           [FromServices] ProjectDbContext context, [AsParameters] PageComponentSearch dataSearch,
           CancellationToken cancellationToken
      )
    {
        try
        {


            var query = context!.Document
                .Skip(dataSearch.Page * dataSearch.PageSize)
                .Take(dataSearch.PageSize);


            if (!string.IsNullOrEmpty(dataSearch.Keyword))
            {
                query = query.Where(x => x.Status.Code == dataSearch.Keyword);
                // query = query.AsNoTracking().Where(p => p.SearchVector.Matches(EF.Functions.PlainToTsQuery("english", dataSearch.Keyword)));
            }

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {

                // var response = securityQuestions.Select(x => ObjectMapper.Mapper.Map<RootDocumentModel>(x));
                var response = securityQuestions.Select(x =>
                 new RootDocumentModel
                 {
                     Id = x.Id.ToString(),
                     DocumentDefinitionId = x.DocumentDefinitionId.ToString(),
                     StatuCode = x.Status.Code,
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
     async ValueTask<IResult> getAllDocumentAll(
           [FromServices] ProjectDbContext context ,
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
                     StatuCode = x.Status.Code,
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
    }

}






