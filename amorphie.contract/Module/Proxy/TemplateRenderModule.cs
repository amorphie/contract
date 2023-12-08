using System;
using System.Net.Http.Json;
using System.Text;
using amorphie.contract.core.Entity.Proxy;
using amorphie.contract.data.Contexts;
using amorphie.contract.RequestModel.Proxy;
using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace amorphie.contract.Module.Proxy
{
    public class TemplateRenderModule : BaseRoute
    {
        public TemplateRenderModule(WebApplication app) : base(app)
        {
        }

        public override string? UrlFragment => "template-render";

        private string TemplateEngineUrl => "https://test-template-engine.burgan.com.tr/";

        public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("render", RenderHtml);
            routeGroupBuilder.MapPost("render/pdf", RenderPdf);
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

                    HttpResponseMessage response = await client.PostAsync(TemplateEngineUrl + "Template/Render", httpContent);

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
                        return Results.Ok(responseBody);
                    }
                    else
                    {
                        return Results.NoContent();
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception();
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

                    HttpResponseMessage response = await client.PostAsync(TemplateEngineUrl + "Template/Render/pdf", httpContent);

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
                        return Results.Ok(responseBody);
                    }
                    else
                    {
                        return Results.NoContent();
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception();
                }
            }

        }
    }
}

