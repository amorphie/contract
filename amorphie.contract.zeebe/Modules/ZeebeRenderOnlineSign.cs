using System.Text.Json;
using amorphie.contract.application;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Dto.Zeebe;
using amorphie.contract.application.Documents.Dto.Zeebe;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Model;
using amorphie.contract.core.Model.Proxy;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.zeebe.Extensions.HeaderHelperZeebe;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using static amorphie.contract.application.DocumentAppService;

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
            app.MapPost("/get-documents-to-approve", GetDocumentsToApprove)
                       .Produces(StatusCodes.Status200OK)
                       .WithOpenApi(operation =>
                       {
                           operation.Summary = "Get Documents To Approve on Zeebe";
                           operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeRenderOnlineSign) } };

                           return operation;
                       });

        }
        static async ValueTask<IResult> Render([FromBody] dynamic body, [FromServices] ITemplateEngineAppService templateEngineAppService)
        {

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var headerModel = HeaderHelperZeebe.GetHeader(body);

            var inputDto = ZeebeMessageHelper.MapToDto<RenderInputDto>(body) as RenderInputDto;

            var contractOutPutDto = ZeebeMessageHelper.MapToDto<ContractInstanceDto>(body, ZeebeConsts.ContractOutputDto) as ContractInstanceDto;

            var documentForApproval = ZeebeMessageHelper.MapToDto<List<DocumentForApproval>>(body, ZeebeConsts.RenderedDocumentsForApproval) as List<DocumentForApproval>;
            
            List<DocumentForApproval> documentForApprovalList = new();
            ContractWithoutHeaderDto? withoutHeaderDto = null;

            if (documentForApproval is null)
            {
                if (String.IsNullOrEmpty(headerModel.UserReference))
                {
                    withoutHeaderDto = HeaderHelperZeebe.SetAndGetHeaderFromWithoutDto(body, headerModel);
                }
                if (inputDto.DocumentList.IsNotEmpty())
                {

                    var tasks = inputDto.DocumentList
                        .Where(x => x.Status != ApprovalStatus.Approved.ToString() && x.DocumentDetail is not null)
                        .Select(async _document =>
                        {
                            var approvedTemplateDocument = new DocumentForApproval
                            {
                                ContractInstanceId = inputDto.ContractInstanceId,
                                ContractCode = inputDto.ContractCode,
                                DocumentSemanticVersion = _document.LastVersion,
                                SemanticVersion = _document.DocumentDetail.OnlineSign.Version,
                                Name = _document.DocumentDetail.OnlineSign.TemplateCode,
                                RenderId = Guid.NewGuid(),
                                RenderData = "{\"customer\":{\"customerIdentity\":\"" + headerModel.UserReference + "\"}, \"customerIdentity\":" + headerModel.UserReference + "}",
                                RenderDataForLog = "{\"customer\":{\"customerIdentity\":\"" + headerModel.UserReference + "\"}, \"customerIdentity\":" + headerModel.UserReference + "}",
                                ProcessName = nameof(ZeebeRenderOnlineSign),
                                Identity = headerModel.UserReference,
                                DocumentDefinitionCode = _document.Code,
                                Approved = false,
                                IsRequired = _document.IsRequired,
                                Title = _document.Name,
                            };

                            var res = await templateEngineAppService.SendRenderPdf(new TemplateRenderRequestModel(approvedTemplateDocument));
                            if (res.IsSuccess)
                            {
                                lock (documentForApprovalList)
                                {
                                    documentForApprovalList.Add(approvedTemplateDocument);
                                }
                            }
                            else
                            {
                                Log.Error("failed to send render pdf. Template.Name = {TemplateCode}  TemplateErrorMessage = {ErrorMessage}", _document.DocumentDetail.OnlineSign.TemplateCode, res.ErrorMessage);
                            }
                        }).ToList();

                    await Task.WhenAll(tasks);

                }
                else
                {
                    Log.Warning("Renderlist Is Not Empty");

                }
            }
            else
            {
                var renderInputDto = inputDto.DocumentList.Where(x => x.Status != ApprovalStatus.Approved.ToString()).Select(x => new { x.Code, x.LastVersion }).ToList();
                documentForApprovalList = documentForApproval.Where(x => renderInputDto.Any(d => d.Code == x.DocumentDefinitionCode && d.LastVersion == x.DocumentSemanticVersion)).ToList();
            }

            messageVariables.Variables.Add(ZeebeConsts.RenderedDocumentsForApproval, documentForApprovalList);

            messageVariables.SetAdditionalData(new RenderApprovalDocument(documentForApprovalList, contractOutPutDto?.DocumentApprovedList, withoutHeaderDto));

            messageVariables.Success = true;

            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }


        static async ValueTask<IResult> GetDocumentsToApprove([FromBody] dynamic body, [FromServices] IDocumentAppService documentAppService)
        {

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var headerModel = HeaderHelperZeebe.GetHeader(body);

            var inputDto = ZeebeMessageHelper.MapToDto<GetDocumentsToApproveInputDtoZeebe>(body, ZeebeConsts.GetDocumentsToApproveInputDto) as GetDocumentsToApproveInputDtoZeebe;

            if (String.IsNullOrEmpty(headerModel.UserReference))
            {
                HeaderHelperZeebe.SetAndGetHeaderFromWithoutDto(body, headerModel);
            }

            var getDto = new GetDocumentsToApproveInputDto
            {
                DocumentCodes = inputDto.DocumentList
            };

            getDto.SetHeaderModel(headerModel);

            var responseDocumentList = await documentAppService.GetDocumentsToApprove(getDto);

            messageVariables.Variables.Add(ZeebeConsts.DocumentListToApprove, responseDocumentList.Data);
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
        static async ValueTask<IResult> Validated([FromBody] dynamic body, [FromServices] IDocumentAppService documentAppService, CancellationToken token)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var headerModel = HeaderHelperZeebe.GetHeader(body);

            var inputDto = ZeebeMessageHelper.MapToDto<ValidatedInputDto>(body);

            if (String.IsNullOrEmpty(headerModel.UserReference))
            {
                HeaderHelperZeebe.SetAndGetHeaderFromWithoutDto(body, headerModel);
            }

            List<ApproveDocumentInstanceInputDto> approvedDocumentIntances = new();
            foreach (var docInstanceId in inputDto.DocumentInstanceIds)
            {
                var approveInput = new ApproveDocumentInstanceInputDto
                {
                    DocumentInstanceId = docInstanceId,
                    ContractCode = inputDto.ContractCode,
                    ContractInstanceId = inputDto.ContractInstanceId
                };

                approveInput.SetHeaderModel(headerModel);

                var res = await documentAppService.ApproveInstance(approveInput);

                if (res.IsSuccess)
                    approvedDocumentIntances.Add(approveInput);
            }


            messageVariables.SetAdditionalData(new ValidatedDocument(approvedDocumentIntances));

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