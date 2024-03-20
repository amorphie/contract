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
            string reference = body.GetProperty("Headers").GetProperty("user_reference").ToString();
            string language = body.GetProperty("Headers").GetProperty("acceptlanguage").ToString();
            string bankEntity = body.GetProperty("Headers").GetProperty("business_line").ToString();
            var documentRenderList = new List<ApprovedTemplateDocumentList>();
            if (messageVariables.TransitionName == "render-online-sign-start")
            {
                var documentDef = messageVariables.Data.GetProperty("entityData").GetProperty("Document").ToString();
                var documentDefDto = JsonSerializer.Deserialize<List<DocumentDef>>(documentDef, options: new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) as List<DocumentDef>;

                var documentDefListCode = documentDefDto
                                        .Select(x => x.DocumentDefinitionCode)
                                        .ToList();

                var documentDefs = dbContext.DocumentDefinition
                    .Where(x => documentDefListCode.Contains(x.Code))
                    .ToList();

                documentRenderList = documentDefs
                  .Where(x => documentDefDto.Any(y => y.DocumentDefinitionCode == x.Code && (y.DocumentSemanticVersion != null ? y.DocumentSemanticVersion == x.Semver : true)))
                  .GroupBy(x => x.Code)
                  .Select(g => g.OrderByDescending(x => x.Semver).FirstOrDefault())
                  .Select(x =>
                  new ApprovedTemplateDocumentList
                  {
                      SemanticVersion = x.DocumentOnlineSing?.DocumentTemplateDetails.FirstOrDefault(z => z.DocumentTemplate.LanguageType.Code == language)?.DocumentTemplate.Version
                         ?? x.DocumentOnlineSing?.DocumentTemplateDetails.FirstOrDefault()?.DocumentTemplate.Version,
                      Name = x.DocumentOnlineSing?.DocumentTemplateDetails.FirstOrDefault(z => z.DocumentTemplate.LanguageType.Code == language)?.DocumentTemplate.Code
                         ?? x.DocumentOnlineSing?.DocumentTemplateDetails.FirstOrDefault()?.DocumentTemplate.Code,
                      RenderId = Guid.NewGuid(),
                      RenderData = "{\"customer\":{\"customerIdentity\":\"" + reference + "\"}, \"customerIdentity\":" + reference + "}",
                      RenderDataForLog = "{\"customer\":{\"customerIdentity\":\"" + reference + "\"}, \"customerIdentity\":" + reference + "}",
                      // Action = "Contract:" + contractDto.Code + ", DocumentDefinition:" + x.DocumentDefinition.Code,
                      ProcessName = nameof(ZeebeRenderOnlineSign),
                      Identity = reference,
                      DocumentDefinitionCode = x.Code,
                      DocumentSemanticVersion = x.Semver,
                      Approved = false,
                  }
              )
                  .ToList();

                dbContext.DocumentDefinition.Where(x => documentDefListCode.Contains(x.Code));
            }
            else
            {

                string contractName = body.GetProperty("ContractInstance").GetProperty("contractName").ToString();
                var contractInstanceId = ZeebeMessageHelper.StringToGuid(body.GetProperty("InstanceId").ToString());
                // messageVariables.TransitionName = "checking-account-opening-start";
                var contractInstance = body.GetProperty("XContractInstance");
                ContractInstanceDto contractDto = JsonSerializer.Deserialize<ContractInstanceDto>(contractInstance, options: new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (contractDto != null)
                {
                    documentRenderList = contractDto.Document.Select(contractDocument => new ApprovedTemplateDocumentList
                    {
                        ContractInstanceId = contractInstanceId,
                        DocumentSemanticVersion = contractDocument.MinVersion,
                        SemanticVersion = contractDocument.DocumentDetail.OnlineSing.Version,
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



                }
            }

            var list = new ApprovedDocumentList();
            foreach (var cdto in documentRenderList)
            {
                HttpSendTemplate(new TemplateRenderRequestModel(cdto));//TODO:dapr yok
                list.Document.Add(new ApprovedDocument
                {
                    DocumentDefinitionCode = cdto.DocumentDefinitionCode,
                    DocumentSemanticVersion = cdto.DocumentSemanticVersion,
                    ContractInstanceId = cdto.ContractInstanceId,
                    RenderId = cdto.RenderId,
                    Approved = cdto.Approved,
                });
            }
            messageVariables.Variables.Add("documentRenderList", documentRenderList);
            messageVariables.Variables.Add("ApprovedDocumentList", list);
            messageVariables.additionalData = list;
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

            // string reference = "";
            long? customerNo = null;
            // if (body.ToString().IndexOf("ContractInstance") != -1)
            // {
            //     if (body.GetProperty("ContractInstance").ToString().IndexOf("reference") != -1)
            //     {
            //         reference = body.GetProperty("ContractInstance").GetProperty("reference").ToString();
            //     }
            // }
            string language = body.GetProperty("Headers").GetProperty("acceptlanguage").ToString();
            string bankEntity = body.GetProperty("Headers").GetProperty("business_line").ToString();
            string reference = body.GetProperty("Headers").GetProperty("user_reference").ToString();

            var headerModel = HeaderHelperZeebe.GetHeader(body);

            if (string.IsNullOrEmpty(reference))
            {
                reference = headerModel.UserReference;
            }
            customerNo = headerModel.CustomerNo;

            // var approvedDocumentList = body.GetProperty("ApprovedDocumentList");
            var approvedDocumentList = messageVariables.Data.GetProperty("entityData");

            var contractDocumentModel = JsonSerializer.Deserialize<ApprovedDocumentList>(approvedDocumentList, options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) as ApprovedDocumentList;
            foreach (var i in contractDocumentModel.Document)
            {
                var input = new DocumentInstanceInputDto
                {
                    Id = i.RenderId,
                    DocumentCode = i.DocumentDefinitionCode,
                    DocumentVersion = i.DocumentSemanticVersion,
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