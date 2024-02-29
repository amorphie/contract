using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;
using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Model;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace amorphie.contract.zeebe.Modules
{
    public static class ZeebeContractInstance
    {
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
            var messageVariables = new MessageVariables();
            try
            {
                messageVariables = ZeebeMessageHelper.VariablesControl(body);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
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
            contract.SetHeaderParameters(new HeaderFilterModel
            {
                UserReference = reference,
                LangCode = language,
                EBankEntity = EBankEntity.on
            });


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
            contract.SetHeaderParameters(new HeaderFilterModel
            {
                UserReference = reference,
                LangCode = language,
                EBankEntity = EBankEntity.on
            });


            var response = await contractAppService.InstanceState(contract, token);
            messageVariables.Variables.Add("ContractInstanceStateResult", response);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }
        static IResult ContractInstance(
          [FromBody] dynamic body,
         [FromServices] ProjectDbContext dbContext,
          HttpRequest request,
          HttpContext httpContext,
          [FromServices] DaprClient client
          , IConfiguration configuration,
          [FromServices] IContractAppService contractAppService, CancellationToken token
      )
        {
            var messageVariables = new MessageVariables();
            try
            {
                messageVariables = ZeebeMessageHelper.VariablesControl(body);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

            try
            {
                // messageVariables.LastTransition = "contract-start-StartContract";
                // messageVariables.TransitionName = "checking-account-opening-start";
                // dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                string reference = body.GetProperty("ContractInstance").GetProperty("reference").ToString();
                string contractName = body.GetProperty("ContractInstance").GetProperty("contractName").ToString();
                string language = body.GetProperty("ContractInstance").GetProperty("language").ToString();
                string bankEntity = body.GetProperty("ContractInstance").GetProperty("bankEntity").ToString();
                var contract = new ContractInstanceInputDto
                {
                    ContractName = contractName,
                };
                contract.SetHeaderParameters(new HeaderFilterModel
                {
                    UserReference = reference,
                    LangCode = language,
                    EBankEntity = EBankEntity.on
                });

                var InstanceDto = contractAppService.Instance(contract, token).Result;
                messageVariables.Variables.Add("XContractInstance", InstanceDto);

                if (InstanceDto.Status.ToString() == EStatus.Completed.ToString())
                {
                    //  messageVariables.TransitionName = "contract-start-StartContract";
                }

                messageVariables.Variables.Add("ContractStatus", InstanceDto.Status);
                messageVariables.Success = true;
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "ErrorUploaded";

                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }
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
            var messageVariables = new MessageVariables();
            try
            {
                messageVariables = ZeebeMessageHelper.VariablesControl(body);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

            try
            {
                dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                string reference = entityData.GetProperty("reference").ToString();
                string deviceId = entityData.GetProperty("deviceId").ToString();
                messageVariables.Success = true;
                messageVariables.LastTransition = "TimeoutRenderOnlineSign";
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "TimeoutRenderOnlineSign";

                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }
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
            var messageVariables = new MessageVariables();
            try
            {
                messageVariables = ZeebeMessageHelper.VariablesControl(body);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

            try
            {
                dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                string reference = entityData.GetProperty("reference").ToString();
                string deviceId = entityData.GetProperty("deviceId").ToString();
                messageVariables.Success = true;
                messageVariables.LastTransition = "DeleteRenderOnlineSign";
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "DeleteRenderOnlineSign";

                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }
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
            var messageVariables = new MessageVariables();
            try
            {
                messageVariables = ZeebeMessageHelper.VariablesControl(body);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

            try
            {
                dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                string reference = entityData.GetProperty("reference").ToString();
                string deviceId = entityData.GetProperty("deviceId").ToString();
                messageVariables.Success = true;
                messageVariables.LastTransition = "ErrorRenderOnlineSign";
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "ErrorRenderOnlineSign";

                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }
        }
    }
}