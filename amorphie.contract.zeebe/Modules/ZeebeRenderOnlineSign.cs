using System.Text;
using System.Text.Json;
using amorphie.contract.application;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.zeebe.Extensions.HeaderHelperZeebe;
using amorphie.contract.zeebe.Model;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace amorphie.contract.zeebe.Modules
{
    public static class ZeebeRenderOnlineSign
    {
        public static void MapZeebeRenderOnlineSignEndpoints(this WebApplication app)
        {
            app.MapPost("/render", Render)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps Render service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeRenderOnlineSign) } };

                return operation;
            });

            app.MapPost("/render-online-sign-not-validated", NotValidated)
                 .Produces(StatusCodes.Status200OK)
                 .WithOpenApi(operation =>
                 {
                     operation.Summary = "Maps NotValidated service worker on Zeebe";
                     operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeRenderOnlineSign) } };

                     return operation;
                 });
            app.MapPost("/render-online-sign-validated", Validated)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps Validated service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeRenderOnlineSign) } };

                return operation;
            });


            app.MapPost("/TimeoutRenderOnlineSign", TimeoutRenderOnlineSign)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps TimeoutRenderOnlineSign service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeRenderOnlineSign) } };

            return operation;
        });
            app.MapPost("/DeleteRenderOnlineSign", DeleteRenderOnlineSign)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps DeleteRenderOnlineSign service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeRenderOnlineSign) } };

            return operation;
        });
            app.MapPost("/ErrorRenderOnlineSign", ErrorRenderOnlineSign)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps ErrorRenderOnlineSign service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeRenderOnlineSign) } };

            return operation;
        });
        }
        static IResult Render(
          [FromBody] dynamic body,
         [FromServices] ProjectDbContext dbContext,
          HttpRequest request,
          HttpContext httpContext,
          [FromServices] DaprClient client
          , IConfiguration configuration
      )
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            string contractName = body.GetProperty("ContractInstance").GetProperty("contractName").ToString();
            string reference = body.GetProperty("ContractInstance").GetProperty("reference").ToString();
            string language = body.GetProperty("ContractInstance").GetProperty("language").ToString();
            // messageVariables.TransitionName = "checking-account-opening-start";

            var contractInstance = body.GetProperty("XContractInstance");
            ContractInstanceDto contractDto = JsonSerializer.Deserialize<ContractInstanceDto>(contractInstance, options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (contractDto != null)
            {
                var contractDocument = contractDto.Document.Select(contractDocument => new ApprovedTemplateRenderRequestModel
                {
                    SemanticVersion = contractDocument.MinVersion,
                    Name = contractDocument.DocumentDetail.OnlineSing.TemplateCode,
                    RenderId = Guid.NewGuid(),
                    RenderData = "{\"customer\":{\"customerIdentity\":\"" + reference + "\"}, \"customerIdentity\":" + reference + "}",
                    RenderDataForLog = "{\"customer\":{\"customerIdentity\":\"" + reference + "\"}, \"customerIdentity\":" + reference + "}",
                    // Action = "Contract:" + contractDto.Code + ", DocumentDefinition:" + x.DocumentDefinition.Code,
                    ProcessName = nameof(ZeebeRenderOnlineSign),
                    Identity = reference,
                    DocumentDefinitionCode = contractDocument.Code,
                    Approved = false,

                }).ToList();
                foreach (TemplateRenderRequestModel cdto in contractDocument)
                {
                    HttpSendTemplate(cdto);//TODO:dapr yok

                }
                messageVariables.Variables.Add("ApprovedDocumentList", contractDocument);
                messageVariables.additionalData = contractDocument;
            }

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

        }
        private static async void HttpSendTemplate(TemplateRenderRequestModel requestModel)//TODO:dapr kullanılacak 
        {
            using (HttpClient client = new HttpClient())
            {
                string modelJson = JsonSerializer.Serialize(requestModel);

                HttpContent httpContent = new StringContent(modelJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(StaticValuesExtensions.TemplateEngineUrl + StaticValuesExtensions.TemplateEnginePdfRenderEndpoint, httpContent);

                if (response.IsSuccessStatusCode)
                {
                }

            }
        }
        private static async Task<string> GetRenderInstance(string instance)//TODO:dapr kullanılacak 
        {
            using (HttpClient client = new HttpClient())
            {
                // string modelJson = JsonSerializer.Serialize(requestModel);

                // HttpContent httpContent = new StringContent(modelJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.GetAsync(StaticValuesExtensions.TemplateEngineUrl + string.Format(StaticValuesExtensions.TemplateEngineRenderInstance, instance));

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

            }
            return "Template engine error";
        }
        static IResult NotValidated(
        [FromBody] dynamic body,
       [FromServices] ProjectDbContext dbContext,
        HttpRequest request,
        HttpContext httpContext,
        [FromServices] DaprClient client
        , IConfiguration configuration
    )
        {

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);

            dynamic? entityData = messageVariables.Data.GetProperty("entityData");
            string reference = entityData.GetProperty("reference").ToString();
            string deviceId = entityData.GetProperty("deviceId").ToString();
            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

        }
        static async ValueTask<IResult> Validated(
        [FromBody] dynamic body,
       [FromServices] ProjectDbContext dbContext,
       [FromServices] IDocumentAppService documentAppService,
        HttpRequest request,
        HttpContext httpContext,
        [FromServices] DaprClient client
        , IConfiguration configuration, CancellationToken token
    )
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);

            string reference = "";
            long? customerNo = null;
            if (body.ToString().IndexOf("ContractInstance") != -1)
            {
                if (body.GetProperty("ContractInstance").ToString().IndexOf("reference") != -1)
                {
                    reference = body.GetProperty("ContractInstance").GetProperty("reference").ToString();
                }
            }
            var headerModel = HeaderHelperZeebe.GetHeader(body);

            if (string.IsNullOrEmpty(reference))
            {
                reference = headerModel.UserReference;
            }
            customerNo = headerModel.CustomerNo;

            // var approvedDocumentList = body.GetProperty("ApprovedDocumentList");
            var approvedDocumentList = messageVariables.Data.GetProperty("entityData");

            var contractDocumentModel = JsonSerializer.Deserialize<List<ApprovedTemplateRenderRequestModel>>(approvedDocumentList, options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) as List<ApprovedTemplateRenderRequestModel>;
            foreach (var i in contractDocumentModel.Where(x => x.Approved).ToList())
            {

                var input = new DocumentInstanceInputDto
                {
                    Id = i.RenderId,
                    DocumentCode = i.DocumentDefinitionCode,
                    DocumentVersion = i.SemanticVersion,
                    // Reference = reference,
                    // Owner = reference,
                    FileName = i.DocumentDefinitionCode + ".pdf", //TODO: Degişecek,
                    FileType = "application/pdf",
                    FileContextType = "TemplateRender",//bunu template Id ilede alsın Id yi arkada baska bir workerla çözede bilirsin bakıcam
                    FileContext = i.RenderId.ToString(),

                };

                input.SetHeaderParameters(reference, customerNo);

                var response = await documentAppService.Instance(input);

                messageVariables.Variables.Add("documentAppService.Instance", response);
                if (!response.IsSuccess)
                {
                    throw new InvalidOperationException("Document Instance Not Complated");
                }

            }
            messageVariables.additionalData = contractDocumentModel;
            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

        }



        static IResult TimeoutRenderOnlineSign(
      [FromBody] dynamic body,
     [FromServices] ProjectDbContext dbContext,
      HttpRequest request,
      HttpContext httpContext,
      [FromServices] DaprClient client
      , IConfiguration configuration
    )
        {

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);

            dynamic? entityData = messageVariables.Data.GetProperty("entityData");
            string reference = entityData.GetProperty("reference").ToString();
            string deviceId = entityData.GetProperty("deviceId").ToString();
            messageVariables.Success = true;
            messageVariables.LastTransition = "TimeoutRenderOnlineSign";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

        }
        static IResult DeleteRenderOnlineSign(
      [FromBody] dynamic body,
     [FromServices] ProjectDbContext dbContext,
      HttpRequest request,
      HttpContext httpContext,
      [FromServices] DaprClient client
      , IConfiguration configuration
    )
        {

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);

            dynamic? entityData = messageVariables.Data.GetProperty("entityData");
            string reference = entityData.GetProperty("reference").ToString();
            string deviceId = entityData.GetProperty("deviceId").ToString();
            messageVariables.Success = true;
            messageVariables.LastTransition = "DeleteRenderOnlineSign";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

        }
        static IResult ErrorRenderOnlineSign(
      [FromBody] dynamic body,
     [FromServices] ProjectDbContext dbContext,
      HttpRequest request,
      HttpContext httpContext,
      [FromServices] DaprClient client
      , IConfiguration configuration
    )
        {

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);

            dynamic? entityData = messageVariables.Data.GetProperty("entityData");
            string reference = entityData.GetProperty("reference").ToString();
            string deviceId = entityData.GetProperty("deviceId").ToString();
            messageVariables.Success = true;
            messageVariables.LastTransition = "ErrorRenderOnlineSign";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

        }
    }
}