using System.Text;
using System.Text.Json;
using amorphie.contract.core.Entity.Proxy;
using amorphie.contract.infrastructure.Contexts;
using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.Models.Proxy;
using amorphie.contract.core;
using amorphie.contract.core.Model.Proxy;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;
using amorphie.contract.Extensions;
using amorphie.contract.core.Response;
using amorphie.contract.application.TemplateEngine;

namespace amorphie.contract.Module.Proxy
{
    public class TemplateRenderModule : BaseRoute
    {
        public TemplateRenderModule(WebApplication app) : base(app)
        {
        }

        public override string? UrlFragment => "template-render";

        public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("render", RenderHtml);
            routeGroupBuilder.MapPost("render/pdf", RenderPdf);
            routeGroupBuilder.MapGet("template/definition", GetTemplates);
            routeGroupBuilder.MapGet("instance/{renderId}", GetRender);
            routeGroupBuilder.MapGet("instance/pdf/{renderId}", GetRenderPdf);
        }

        async ValueTask<IResult> RenderHtml(
          [FromServices] ProjectDbContext context,
          [FromServices] ITemplateEngineAppService templateEngineAppService,
          [FromBody] TemplateRenderRequestModel requestModel, HttpContext httpContext,
          CancellationToken cancellationToken)
        {


            var headerModels = HeaderHelper.GetHeader(httpContext);

            if (requestModel.RenderId == Guid.Empty)
            {
                requestModel.RenderId = Guid.NewGuid();
            }
            if (string.IsNullOrEmpty(requestModel.RenderData.ToString()))
            {
                requestModel.RenderData = "{\"customer\":{\"customerIdentity\":\"" + headerModels.UserReference + "\"}, \"customerIdentity\":" + headerModels.UserReference + "}";
                requestModel.RenderDataForLog = "{\"customer\":{\"customerIdentity\":\"" + headerModels.UserReference + "\"}, \"customerIdentity\":" + headerModels.UserReference + "}";
            }
            if (string.IsNullOrEmpty(requestModel.Identity))
            {
                requestModel.Identity = headerModels.UserReference;
            }

            var resultRenderHtml = await templateEngineAppService.SendRenderHtml(requestModel);

            if (resultRenderHtml.IsSuccess)
            {
                TemplateRender renderEntity = new TemplateRender
                {
                    RenderData = JsonSerializer.Serialize(requestModel.RenderData),
                    TemplateName = requestModel.Name,
                    RenderType = "Html"
                };

                await context.TemplateRender.AddAsync(renderEntity);
                context.SaveChanges();

                return Results.Ok(new
                {
                    Data = new { TemplateRenderRequestModel = requestModel, Content = resultRenderHtml.Data },
                    Success = true,
                    ErrorMessage = "",
                });
            }
            else
            {
                return Results.Problem(detail: $"Template Engine Render Exception {resultRenderHtml.ErrorMessage}");
            }


        }

        async ValueTask<IResult> RenderPdf(
          [FromServices] ProjectDbContext context,
          [FromServices] ITemplateEngineAppService templateEngineAppService,
          [FromBody] TemplateRenderRequestModel requestModel, HttpContext httpContext,
          CancellationToken cancellationToken)
        {


            var headerModels = HeaderHelper.GetHeader(httpContext);

            if (requestModel.RenderId == Guid.Empty)
            {
                requestModel.RenderId = Guid.NewGuid();
            }
            if (requestModel.RenderData == null || string.IsNullOrEmpty(requestModel.RenderData.ToString()))
            {
                requestModel.RenderData = "{\"customer\":{\"customerIdentity\":\"" + headerModels.UserReference + "\"}, \"customerIdentity\":" + headerModels.UserReference + "}";
                requestModel.RenderDataForLog = "{\"customer\":{\"customerIdentity\":\"" + headerModels.UserReference + "\"}, \"customerIdentity\":" + headerModels.UserReference + "}";
            }
            if (string.IsNullOrEmpty(requestModel.Identity))
            {
                requestModel.Identity = headerModels.UserReference;
            }

            var resultRenderPdf = await templateEngineAppService.SendRenderPdf(requestModel);

            if (resultRenderPdf.IsSuccess)
            {
                TemplateRender renderEntity = new TemplateRender
                {
                    RenderData = JsonSerializer.Serialize(requestModel.RenderData),
                    TemplateName = requestModel.Name,
                    RenderType = "Pdf"
                };

                await context.TemplateRender.AddAsync(renderEntity);
                context.SaveChanges();
                return Results.Ok(new
                {
                    Data = new { TemplateRenderRequestModel = requestModel, Content = resultRenderPdf.Data },
                    Success = true,
                    ErrorMessage = "",
                });
            }
            else
            {
                return Results.Problem(detail: $"Template Engine Render Exception {resultRenderPdf.ErrorMessage}");
            }

        }

        async ValueTask<IResult> GetTemplates(
          [FromServices] ProjectDbContext context,
          [FromServices] ITemplateEngineAppService templateEngineAppService,
          [FromQuery] string query,
          CancellationToken cancellationToken)
        {


            var response = await templateEngineAppService.GetTemplateDefinitions(query);

            if (response.IsSuccess)
            {
                List<TemplateEngineDefinitionResponseModel> responseList = response.Data;

                var dbQuery = context!.DocumentOnlineSign.AsQueryable();


                // "%" karakteri varsa deseni kontrol et
                if (query.Contains("%"))
                {
                    // Deseni kontrol et ve LIKE operatörünü kullan
                    dbQuery = dbQuery.Where(x =>x.Templates.Any(a=> EF.Functions.Like(a.Code, query)));
                }
                else
                {
                    // "%" karakteri yoksa direkt olarak eşleşmeyi kontrol et
                    dbQuery = dbQuery.Where(x => x.Templates.Any(a=>a.Code == query));
                }

                var dbList = await dbQuery.SelectMany(x => x.Templates.Select(a=>a.Code)).ToListAsync(cancellationToken);

                responseList = responseList.Where(x => !dbList.Contains(x.Name)).ToList();

                return Results.Ok(responseList);
            }
            else
            {
                return Results.Problem(detail: $"Template Engine Render Exception {response.ErrorMessage}");
            }

        }

        async Task<GenericResult<string>> GetRender([FromServices] ITemplateEngineAppService templateEngineAppService, string renderId)
        {
            var result = await templateEngineAppService.GetRender(renderId);
            return result;
        }

        async Task<GenericResult<string>> GetRenderPdf([FromServices] ITemplateEngineAppService templateEngineAppService, string renderId)
        {
            var result = await templateEngineAppService.GetRenderPdf(renderId);
            return result;
        }
    }
}

