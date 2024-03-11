using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.zeebe.Model;
using amorphie.contract.zeebe.Services;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace amorphie.contract.zeebe.Modules.ZeebeDocumentDef
{
    public static class ZeebeDocumentDefinition
    {
        public static void MapZeebeDocumentDefinitionEndpoints(this WebApplication app)
        {
            app.MapPost("/definitionupload", DefinitionUpload)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps Render service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentDefinition) } };

                return operation;
            });

            app.MapPost("/updatedefinitionupload", DefinitionUpdate)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps Render service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentDefinition) } };

                return operation;
            });

            app.MapPost("/ErrorDefinitionUpload", ErrorDefinitionUpload)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps Render service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentDefinition) } };

              return operation;
          });
            app.MapPost("/DeleteProcessDefinitionUpload", DeleteProcessDefinitionUpload)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps Render service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentDefinition) } };

              return operation;
          });
            app.MapPost("/TimeoutDefinitionUpload", TimeoutDefinitionUpload)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps Render service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };

              return operation;
          });

        }
        static IResult DefinitionUpload(
          [FromBody] dynamic body,
         [FromServices] ProjectDbContext dbContext,
          HttpRequest request,
          HttpContext httpContext,
          [FromServices] DaprClient client
          , IConfiguration configuration,
           [FromServices] IDocumentDefinitionService IDocumentDefinitionService
      )
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            dynamic? entityData = messageVariables.Data.GetProperty("entityData").ToString();

            var _ = IDocumentDefinitionService.DataModelToDocumentDefinition(entityData, messageVariables.InstanceIdGuid);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult DefinitionUpdate(
          [FromBody] dynamic body,
         [FromServices] ProjectDbContext dbContext,
          HttpRequest request,
          HttpContext httpContext,
          [FromServices] DaprClient client
          , IConfiguration configuration,
           [FromServices] IDocumentDefinitionService IDocumentDefinitionService
      )
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            dynamic? entityData = messageVariables.Data.GetProperty("entityData").ToString();
            var _ = IDocumentDefinitionService.DataModelToDocumentDefinitionUpdate(entityData, messageVariables.InstanceIdGuid);
            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }
        static IResult TimeoutDefinitionUpload(
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
        static IResult DeleteProcessDefinitionUpload(
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
        static IResult ErrorDefinitionUpload(
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