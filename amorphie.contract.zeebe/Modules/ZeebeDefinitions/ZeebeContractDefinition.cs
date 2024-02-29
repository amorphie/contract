using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Model;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using amorphie.contract.zeebe.Services;

namespace amorphie.contract.zeebe.Modules.ZeebeDocumentDef
{
    public static class ZeebeContractDefinition
    {
        public static void MapZeebeContractDefinitionEndpoints(this WebApplication app)
        {
            app.MapPost("/contractdefinitionupdate", contractdefinitionupdate)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps contractdefinition service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };
                return operation;
            });

            app.MapPost("/contractdefinition", contractdefinition)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps contractdefinition service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };
                return operation;
            });

            app.MapPost("/errorcontractdefinition", errorcontractdefinition)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps errorcontractdefinition service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };

              return operation;
          });
            app.MapPost("/deletecontractdefinition", deletecontractdefinition)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps deletecontractdefinition service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };

              return operation;
          });
            app.MapPost("/timeoutcontractdefinition", timeoutcontractdefinition)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps Render service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };

              return operation;
          });

        }

        static IResult contractdefinitionupdate(
          [FromBody] dynamic body,
         [FromServices] ProjectDbContext dbContext,
          HttpRequest request,
          HttpContext httpContext,
          [FromServices] DaprClient client
          , IConfiguration configuration,
           [FromServices] IContractDefinitionService IContractDefinitionService
      )
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);

            dynamic? entityData = messageVariables.Data.GetProperty("entityData");

            var _ = IContractDefinitionService.DataModelToContractDefinitionUpdate(entityData, messageVariables.InstanceIdGuid);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
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
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);


            dynamic? entityData = messageVariables.Data.GetProperty("entityData");

            var _ = IContractDefinitionService.DataModelToContractDefinition(entityData, messageVariables.InstanceIdGuid);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
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
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "TimeoutDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));


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
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "DeleteProcessDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
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
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "ErrorDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }



    }
}