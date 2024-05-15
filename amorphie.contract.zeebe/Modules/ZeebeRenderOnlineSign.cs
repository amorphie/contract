using System.Text.Json;
using amorphie.contract.application;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Dto.Zeebe;
using amorphie.contract.application.Documents.Dto.Zeebe;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Model;
using amorphie.contract.core.Model.Proxy;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.zeebe.Extensions.HeaderHelperZeebe;
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
            [FromServices] ITemplateEngineAppService templateEngineAppService)
        {

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var headerModel = HeaderHelperZeebe.GetHeader(body);

            var inputDto = ZeebeMessageHelper.MapToDto<RenderInputDto>(body);

            if (String.IsNullOrWhiteSpace(headerModel.UserReference))
            {
                var contractWithoutHeaderDto = ZeebeMessageHelper.MapToDto<ContractWithoutHeaderDto>(body, ZeebeConsts.ContractWithoutHeaderDto);
                headerModel.UserReference = contractWithoutHeaderDto.Reference;
                var banktEntity = headerModel.GetBankEntity(contractWithoutHeaderDto.BankEntity);
                headerModel.SetBankEntity(banktEntity);
            }

            List<ApprovedTemplateDocumentList> approvedTemplateDocumentList = new();

            if (inputDto.DocumentList.IsNotEmpty())
            {

                foreach (var _document in inputDto.DocumentList)
                {
                    if (_document.DocumentDetail is not null)
                    {
                        // ContractOutput ile aldığımız değerler.
                        approvedTemplateDocumentList.Add(new ApprovedTemplateDocumentList
                        {
                            ContractInstanceId = inputDto.ContractInstanceId,
                            DocumentSemanticVersion = _document.MinVersion,
                            SemanticVersion = _document.DocumentDetail.OnlineSign.Version,
                            Name = _document.DocumentDetail.OnlineSign.TemplateCode,
                            RenderId = Guid.NewGuid(),
                            RenderData = "{\"customer\":{\"customerIdentity\":\"" + headerModel.UserReference + "\"}, \"customerIdentity\":" + headerModel.UserReference + "}",
                            RenderDataForLog = "{\"customer\":{\"customerIdentity\":\"" + headerModel.UserReference + "\"}, \"customerIdentity\":" + headerModel.UserReference + "}",
                            ProcessName = nameof(ZeebeRenderOnlineSign),
                            Identity = headerModel.UserReference,
                            DocumentDefinitionCode = _document.Code,
                            Approved = false,
                        });
                    }
                    else
                    {
                        //Direkt render ile bekledğimiz iki değer var.
                        // DB den onlinesign kayıtlarını çek 
                    }
                }
            }


            var subFlowContractInstance = false;

            var documentRenderList = new List<ApprovedTemplateDocumentList>();

            if (inputDto.IsRenderOnlineMainFlow) //messageVariables.TransitionName == "render-online-sign-start" && !subFlowContractInstance)
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
                      SemanticVersion = x.DocumentOnlineSign?.Templates.FirstOrDefault(z => z.LanguageCode == headerModel.LangCode)?.Version
                         ?? x.DocumentOnlineSign?.Templates.FirstOrDefault()?.Version,
                      Name = x.DocumentOnlineSign?.Templates.FirstOrDefault(z => z.LanguageCode == headerModel.LangCode)?.Code
                         ?? x.DocumentOnlineSign?.Templates.FirstOrDefault()?.Code,
                      RenderId = Guid.NewGuid(),
                      RenderData = "{\"customer\":{\"customerIdentity\":\"" + headerModel.UserReference + "\"}, \"customerIdentity\":" + headerModel.UserReference + "}",
                      RenderDataForLog = "{\"customer\":{\"customerIdentity\":\"" + headerModel.UserReference + "\"}, \"customerIdentity\":" + headerModel.UserReference + "}",
                      // Action = "Contract:" + contractDto.Code + ", DocumentDefinition:" + x.DocumentDefinition.Code,
                      ProcessName = nameof(ZeebeRenderOnlineSign),
                      Identity = headerModel.UserReference,
                      DocumentDefinitionCode = x.Code,
                      DocumentSemanticVersion = x.Semver,
                      Approved = false,
                  }
              )
                  .ToList();

                dbContext.DocumentDefinition.Where(x => documentDefListCode.Contains(x.Code));
            }


            var list = new ApprovedDocumentList();
            foreach (var cdto in documentRenderList)
            {
                var renderRequestModel = new TemplateRenderRequestModel(cdto);
                templateEngineAppService.SendRenderPdf(renderRequestModel);
                //TODO render edilip edilmediği hata kontrolü yok.
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
            HeaderFilterModel headerModel;
            headerModel = HeaderHelperZeebe.GetHeader(body);


            if (body.ToString().IndexOf("XContractInstance") != -1)

            {
                if (body.GetProperty("XContractInstance").ToString().IndexOf("reference") != -1)
                {
                    headerModel.UserReference = body.GetProperty("XContractInstance").GetProperty("reference").ToString();
                }
                if (body.GetProperty("XContractInstance").ToString().IndexOf("language") != -1)
                {
                    headerModel.LangCode = body.GetProperty("XContractInstance").GetProperty("language").ToString();
                }
                if (body.GetProperty("XContractInstance").ToString().IndexOf("bankEntity") != -1)
                {
                    headerModel.GetBankEntity(body.GetProperty("XContractInstance").GetProperty("bankEntity").ToString());
                }
            }


            customerNo = headerModel.CustomerNo;

            // var approvedDocumentList = body.GetProperty("ApprovedDocumentList");
            var approvedDocumentList = messageVariables.Data.GetProperty("entityData");

            var contractDocumentModel = JsonSerializer.Deserialize<ApprovedDocumentList>(approvedDocumentList, options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) as ApprovedDocumentList;

            // foreach (var i in contractDocumentModel.Document.Where(x => x.Approved))
            // {
            //     var input = new DocumentInstanceInputDto
            //     {
            //         Id = i.RenderId,
            //         DocumentCode = i.DocumentDefinitionCode,
            //         DocumentVersion = i.DocumentSemanticVersion,
            //         DocumentContent = new DocumentContentDto
            //         {
            //             FileName = i.DocumentDefinitionCode + ".pdf",
            //             ContentType = "application/pdf",
            //             FileContext = i.RenderId.ToString(),
            //         },
            //         ContextType = AppConsts.ConverterTemplateRender
            //     };

            //     input.SetHeaderParameters(headerModel.UserReference, customerNo);

            //     await documentAppService.Instance(input);

            // }
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