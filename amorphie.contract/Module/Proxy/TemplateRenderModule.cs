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
          [FromBody] TemplateRenderRequestModel requestModel, HttpContext httpContext,
          CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
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


                    string modelJson = JsonSerializer.Serialize(requestModel);

                    HttpContent httpContent = new StringContent(modelJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(StaticValuesExtensions.TemplateEngineUrl + StaticValuesExtensions.TemplateEngineHtmlRenderEndpoint, httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
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
                            Data = new { TemplateRenderRequestModel = requestModel, Content = responseBody.Trim('\"') },
                            Success = true,
                            ErrorMessage = "",
                        });
                    }
                    else
                    {
                        string exception = await response.Content.ReadAsStringAsync();
                        dynamic exObject = Newtonsoft.Json.JsonConvert.DeserializeObject(exception);
                        return Results.Problem(detail: "Template Engine Render Exception", statusCode: (int)exObject?.status);
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Template Engine Connection Exception");
                }
            }

        }

        async ValueTask<IResult> RenderPdf(
          [FromServices] ProjectDbContext context,
          [FromBody] TemplateRenderRequestModel requestModel, HttpContext httpContext,
          CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
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
                    string modelJson = JsonSerializer.Serialize(requestModel);

                    HttpContent httpContent = new StringContent(modelJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(StaticValuesExtensions.TemplateEngineUrl + StaticValuesExtensions.TemplateEnginePdfRenderEndpoint, httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
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
                            Data = new { TemplateRenderRequestModel = requestModel, Content = responseBody.Trim('\"') },
                            Success = true,
                            ErrorMessage = "",
                        });
                    }
                    else
                    {
                        string exception = await response.Content.ReadAsStringAsync();
                        dynamic exObject = Newtonsoft.Json.JsonConvert.DeserializeObject(exception);
                        return Results.Problem(detail: "Template Engine Render Exception", statusCode: (int)exObject?.status);
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Template Engine Connection Exception");
                }
            }

        }

        async ValueTask<IResult> GetTemplates(
          [FromServices] ProjectDbContext context,
          [FromQuery] string query,
          CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(StaticValuesExtensions.TemplateEngineUrl + StaticValuesExtensions.TemplateEngineGetTemplateEndpoint + "?query=" + query);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (string.IsNullOrEmpty(responseBody))
                        {
                            return Results.NoContent();
                        }

                        Dictionary<string, List<TemplateEngineDefinitionResponseModel>> responseDictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<TemplateEngineDefinitionResponseModel>>>(responseBody);
                        List<TemplateEngineDefinitionResponseModel> responseList = responseDictionary["templateDefinitionNames"];

                        var dbQuery = context!.DocumentTemplate.AsQueryable();


                        // "%" karakteri varsa deseni kontrol et
                        if (query.Contains("%"))
                        {
                            // Deseni kontrol et ve LIKE operatörünü kullan
                            dbQuery = dbQuery.Where(x => EF.Functions.Like(x.Code, query));
                        }
                        else
                        {
                            // "%" karakteri yoksa direkt olarak eşleşmeyi kontrol et
                            dbQuery = dbQuery.Where(x => x.Code == query);
                        }

                        var dbList = await dbQuery.Select(x => x.Code).ToListAsync(cancellationToken);

                        responseList = responseList.Where(x => !dbList.Contains(x.Name)).ToList();

                        return Results.Ok(responseList);
                    }
                    else
                    {
                        string exception = await response.Content.ReadAsStringAsync();
                        dynamic exObject = Newtonsoft.Json.JsonConvert.DeserializeObject(exception);
                        return Results.Problem(detail: "Template Engine Render Exception", statusCode: (int)exObject?.status);
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Template Engine Connection Exception");
                }
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

