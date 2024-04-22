using amorphie.contract.application;
using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Dto.Zeebe;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.core.Model;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.zeebe.Extensions.HeaderHelperZeebe;
using amorphie.contract.zeebe.Model;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace amorphie.contract.zeebe.Modules
{
    public static class ZeebeContractInstance
    {
        public const string ContractVariableName = "ContractInstance";
        public const string XContractOutputDto = "XContractOutputDto";
        public const string ContractStatus = "ContractStatus";



        public static void MapZeebeContractInstanceEndpoints(this WebApplication app)
        {
            app.MapPost("/startcontract", StartContract)
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
            app.MapPost("/contractinstancestate2", ContractInstanceState2)
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
        static IResult StartContract(
                 [FromBody] dynamic body,
                [FromServices] ProjectDbContext dbContext,
                 HttpRequest request,
                 HttpContext httpContext,
                 [FromServices] DaprClient client
                 , IConfiguration configuration
             )
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

        }
        static async ValueTask<IResult> ContractInstanceState(
    [FromBody] dynamic body,
   [FromServices] ProjectDbContext dbContext,
    HttpRequest request,
    HttpContext httpContext,
    [FromServices] DaprClient client
    , IConfiguration configuration,
    [FromServices] IContractAppService contractAppService, CancellationToken token
)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var contractName = body.GetProperty("ContractInstanceState").GetProperty("contractName").ToString();
            var reference = body.GetProperty("ContractInstanceState").GetProperty("reference").ToString();
            var language = body.GetProperty("ContractInstanceState").GetProperty("language").ToString();
            string bankEntity = body.GetProperty("ContractInstanceState").GetProperty("bankEntity").ToString();
            var contract = new ContractInstanceInputDto
            {
                ContractName = contractName,
            };
            contract.SetHeaderParameters(new HeaderFilterModel(bankEntity, language, "", reference, null));

            var response = await contractAppService.InstanceState(contract, token);
            messageVariables.Variables.Add("ContractInstanceStateResult", response);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }
        static async ValueTask<IResult> ContractInstanceState2(
   [FromBody] dynamic body,
  [FromServices] ProjectDbContext dbContext,
   HttpRequest request,
   HttpContext httpContext,
   [FromServices] DaprClient client
   , IConfiguration configuration,
   [FromServices] IContractAppService contractAppService, CancellationToken token
)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var contractName = body.GetProperty("ContractInstanceState").GetProperty("contractName").ToString();
            var reference = body.GetProperty("ContractInstanceState").GetProperty("reference").ToString();
            var language = body.GetProperty("ContractInstanceState").GetProperty("language").ToString();
            string bankEntity = body.GetProperty("ContractInstanceState").GetProperty("bankEntity").ToString();
            var contract = new ContractInstanceInputDto
            {
                ContractName = contractName,
            };
            contract.SetHeaderParameters(new HeaderFilterModel(bankEntity, language, "", reference, null));


            var response = await contractAppService.InstanceState(contract, token);
            messageVariables.Variables.Add("ContractInstanceStateResult", response);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }



        static async ValueTask<IResult> ContractInstance([FromBody] dynamic body, [FromServices] IContractAppService contractAppService,
        [FromServices] IUserSignedContractAppService userSignedContractAppService, CancellationToken token)
        {

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var headerModel = HeaderHelperZeebe.GetHeader(body);

            var inputDto = ZeebeMessageHelper.MapToDto<ContractInputDto>(body);

            var contractServiceInput = new ContractInstanceInputDto
            {
                ContractName = inputDto.ContractCode,
                ContractInstanceId = ZeebeMessageHelper.StringToGuid(inputDto.ContractInstanceId),
            };

            if (String.IsNullOrEmpty(headerModel.UserReference))
            {
                var contractInstanceDtoZeebe = ZeebeMessageHelper.MapToDto<ContractInstanceZeebeDto>(body, ContractVariableName);
                headerModel.UserReference = contractInstanceDtoZeebe.Reference;
                var banktEntity = headerModel.GetBankEntity(contractInstanceDtoZeebe.BankEntity);
                headerModel.SetBankEntity(banktEntity);
            }

            contractServiceInput.SetHeaderParameters(headerModel);

            var instanceDto = await contractAppService.Instance(contractServiceInput, token);

            messageVariables.Variables.Add("XContractInstance", instanceDto.Data); //TODO kullanılmıyorsa silinecek.

            messageVariables.Variables.Add(XContractOutputDto, instanceDto.Data);

            messageVariables.Variables.Add(ContractStatus, instanceDto.Data.Status);

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