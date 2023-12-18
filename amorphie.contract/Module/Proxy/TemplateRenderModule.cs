using System;
using System.Net.Http.Json;
using System.Text;
using amorphie.contract.core.Entity.Proxy;
using amorphie.contract.data.Contexts;
using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using amorphie.contract.Extensions;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.Models.Proxy;

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
        }

        async ValueTask<IResult> RenderHtml(
          [FromServices] ProjectDbContext context,
          [FromBody] TemplateRenderRequestModel requestModel,
          CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string modelJson = JsonConvert.SerializeObject(requestModel);

                    HttpContent httpContent = new StringContent(modelJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(StaticValuesExtensions.TemplateEngineUrl + StaticValuesExtensions.TemplateEngineHtmlRenderEndpoint, httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        TemplateRender renderEntity = new TemplateRender
                        {
                            RenderData = JsonConvert.SerializeObject(requestModel.RenderData),
                            TemplateName = requestModel.Name,
                            RenderType = "Html"
                        };

                        await context.TemplateRender.AddAsync(renderEntity);
                        return Results.Ok(responseBody.Trim('\"'));
                    }
                    else
                    {
                        string exception = await response.Content.ReadAsStringAsync();
                        dynamic exObject = JsonConvert.DeserializeObject(exception);
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
          [FromBody] TemplateRenderRequestModel requestModel,
          CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string modelJson = JsonConvert.SerializeObject(requestModel);

                    HttpContent httpContent = new StringContent(modelJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(StaticValuesExtensions.TemplateEngineUrl + StaticValuesExtensions.TemplateEnginePdfRenderEndpoint, httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        TemplateRender renderEntity = new TemplateRender
                        {
                            RenderData = JsonConvert.SerializeObject(requestModel.RenderData),
                            TemplateName = requestModel.Name,
                            RenderType = "Pdf"
                        };

                        await context.TemplateRender.AddAsync(renderEntity);
                        return Results.Ok(responseBody.Trim('\"'));
                    }
                    else
                    {
                        string exception = await response.Content.ReadAsStringAsync();
                        dynamic exObject = JsonConvert.DeserializeObject(exception);
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

                        Dictionary<string, List<TemplateEngineDefinitionResponseModel>> responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<TemplateEngineDefinitionResponseModel>>>(responseBody);
                        List<TemplateEngineDefinitionResponseModel> responseList = responseDictionary["templateDefinitionNames"];

                        var dbQuery = context!.DocumentTemplate.AsQueryable();

                        dbQuery = ContractHelperExtensions.LikeWhere(dbQuery, query);

                        var dbList = await dbQuery.Select(x => x.Code).ToListAsync(cancellationToken);

                        responseList = responseList.Where(x => !dbList.Contains(x.Name)).ToList();

                        responseDictionary["templateDefinitionNames"] = responseList;

                        return Results.Ok(responseDictionary);
                    }
                    else
                    {
                        string exception = await response.Content.ReadAsStringAsync();
                        dynamic exObject = JsonConvert.DeserializeObject(exception);
                        return Results.Problem(detail: "Template Engine Render Exception", statusCode: (int)exObject?.status);
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Template Engine Connection Exception");
                }
            }
        }
    }
}

