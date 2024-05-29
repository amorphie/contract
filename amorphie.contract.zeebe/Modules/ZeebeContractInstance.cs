using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Dto.Zeebe;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.application.DMN.Dto;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Model;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.zeebe.Extensions.HeaderHelperZeebe;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace amorphie.contract.zeebe.Modules
{
    public static class ZeebeContractInstance
    {

        public static void MapZeebeContractInstanceEndpoints(this WebApplication app)
        {
            app.MapPost("/contract-back-transition", ContractBackTransition)
           .Produces(StatusCodes.Status200OK)
           .WithOpenApi(operation =>
           {
               operation.Summary = "Maps ContractInstance service worker on Zeebe";
               operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractInstance) } };
               return operation;
           });
            app.MapPost("/customer-approve-by-contract", CustomerApproveByContract)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps ContractInstance service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractInstance) } };
                return operation;
            });

            app.MapPost("/contractinstance", ContractInstance)
                      .Produces(StatusCodes.Status200OK)
                      .WithOpenApi(operation =>
                      {
                          operation.Summary = "Maps ContractInstance service worker on Zeebe";
                          operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractInstance) } };
                          return operation;
                      });

            app.MapPost("/contractinstancestate", ContractInstanceState)
                                 .Produces(StatusCodes.Status200OK)
                                 .WithOpenApi(operation =>
                                 {
                                     operation.Summary = "Maps ContractInstance service worker on Zeebe";
                                     operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractInstance) } };
                                     return operation;
                                 });

            app.MapPost("/timeoutcontract", TimeoutContract)
                .Produces(StatusCodes.Status200OK)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Maps TimeoutContract service worker on Zeebe";
                    operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractInstance) } };
                    return operation;
                });

            app.MapPost("/deletecontract", DeleteContract)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps DeleteContract service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractInstance) } };
            return operation;
        });
            app.MapPost("/errorcontract", ErrorContract)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps ErrorContract service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractInstance) } };
            return operation;
        });

        }

        static async ValueTask<IResult> ContractInstanceState([FromBody] dynamic body, [FromServices] IContractAppService contractAppService, CancellationToken token)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var headerModel = HeaderHelperZeebe.GetHeader(body);
            var inputDto = ZeebeMessageHelper.MapToDto<ContractInstanceStateInputDto>(body);

            inputDto.SetHeaderParameters(headerModel);

            var response = await contractAppService.InstanceState(inputDto, token);

            messageVariables.Variables.Add(ZeebeConsts.ContractInstanceStateResult, response);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static async ValueTask<IResult> ContractBackTransition([FromBody] dynamic body)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var backTransitionDto = ZeebeMessageHelper.MapToDto<BackTransitionDto>(body) as BackTransitionDto;
            backTransitionDto.BackTransitionId = backTransitionDto.BackTransitionId == null ? ZeebeConsts.ContractStartBack : backTransitionDto.BackTransitionId;
            messageVariables.additionalData = backTransitionDto;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static async ValueTask<IResult> CustomerApproveByContract([FromBody] dynamic body, [FromServices] IContractAppService contractAppService,
               [FromServices] IUserSignedContractAppService userSignedContractAppService, CancellationToken token)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var headerModel = HeaderHelperZeebe.GetHeader(body);

            var inputDto = ZeebeMessageHelper.MapToDto<CustomerApproveByContractInputDto>(body);
            var contractServiceInput = new ContractApprovedAndPendingDocumentsInputDto
            {
                ContractName = inputDto.ContractCode
            };
            contractServiceInput.SetHeaderModel(headerModel);
            var instanceDto = await contractAppService.GetContractApprovedAndPendingDocuments(contractServiceInput, token);

            messageVariables.Variables.Add(ZeebeConsts.CustomerApproveByContractOutputDto, instanceDto.Data);

            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

        }
        static async ValueTask<IResult> ContractInstance([FromBody] dynamic body, [FromServices] IContractAppService contractAppService,
        [FromServices] IUserSignedContractAppService userSignedContractAppService, CancellationToken token)
        {

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var headerModel = HeaderHelperZeebe.GetHeader(body) as HeaderFilterModel;

            var inputDto = ZeebeMessageHelper.MapToDto<ContractInputDto>(body);
            var dmnResult = ZeebeMessageHelper.MapToDto<List<DmnResultDto>>(body, ZeebeConsts.DmnResult) as List<DmnResultDto>;

            var contractServiceInput = new ContractInstanceInputDto
            {
                ContractCode = inputDto.ContractCode,
                ContractInstanceId = ZeebeMessageHelper.StringToGuid(inputDto.ContractInstanceId),
                DmnResult = dmnResult,
            };

            if (String.IsNullOrEmpty(headerModel.UserReference))
            {
                HeaderHelperZeebe.SetHeaderFromWithoutDto(body, headerModel);
            }

            contractServiceInput.SetHeaderModel(headerModel);

            var instanceDto = await contractAppService.Instance(contractServiceInput, token);

            messageVariables.Variables.Add(ZeebeConsts.ContractOutputDto, instanceDto.Data);

            messageVariables.Variables.Add(ZeebeConsts.ContractStatus, instanceDto.Data.Status);

            messageVariables.Success = true;

            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }



        static IResult TimeoutContract(
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
        static IResult DeleteContract(
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
        static IResult ErrorContract(
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