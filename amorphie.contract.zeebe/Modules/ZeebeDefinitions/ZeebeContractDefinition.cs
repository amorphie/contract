using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Model;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using amorphie.contract.zeebe.Services.Interfaces;

namespace amorphie.contract.zeebe.Modules.ZeebeDocumentDef
{
    public static class ZeebeContractDefinition
    {
        public static void MapZeebeContractDefinitionEndpoints(this WebApplication app)
        {
            app.MapPost("/contractdefinition", contractdefinition)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps contractdefinition service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = "Zeeb contractdefinition" } };
                return operation;
            });

            app.MapPost("/errorcontractdefinition", errorcontractdefinition)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps errorcontractdefinition service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe errorcontractdefinition" } };
              return operation;
          });
            app.MapPost("/deletecontractdefinition", deletecontractdefinition)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps deletecontractdefinition service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe deletecontractdefinition" } };
              return operation;
          });
            app.MapPost("/timeoutcontractdefinition", timeoutcontractdefinition)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps Render service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
              return operation;
          });

        }
        static IResult contractdefinition(
          [FromBody] dynamic body,
         [FromServices] ProjectDbContext dbContext,
          HttpRequest request,
          HttpContext httpContext,
          [FromServices] DaprClient client
          , IConfiguration configuration,
           [FromServices] IContractDefinitionService IContractDefinitionService
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

                var _ = IContractDefinitionService.DataModelToContractDefinition(entityData, messageVariables.RecordIdGuid);

                messageVariables.Success = true;
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "ErrorDefinition";

                return Results.BadRequest(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }
        }

        static IResult timeoutcontractdefinition(
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
                // dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                // string reference = entityData.GetProperty("reference").ToString();
                // string deviceId = entityData.GetProperty("deviceId").ToString();
                messageVariables.Success = true;
                messageVariables.LastTransition = "TimeoutDefinitionUpload";
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "ErrorDefinitionUpload";

                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }
        }
        static IResult deletecontractdefinition(
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
                // dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                // string reference = entityData.GetProperty("reference").ToString();
                // string deviceId = entityData.GetProperty("deviceId").ToString();
                messageVariables.Success = true;
                messageVariables.LastTransition = "DeleteProcessDefinitionUpload";
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "ErrorDefinitionUpload";

                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }
        }
        static IResult errorcontractdefinition(
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
                // dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                // string reference = entityData.GetProperty("reference").ToString();
                // string deviceId = entityData.GetProperty("deviceId").ToString();
                messageVariables.Success = true;
                messageVariables.LastTransition = "ErrorDefinitionUpload";
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "ErrorDefinitionUpload";

                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }
        }
    }



}