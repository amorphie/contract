using System.Text.Json;
using amorphie.contract.application;
using amorphie.contract.zeebe.Services;
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
        static IResult DefinitionUpload([FromBody] dynamic body, [FromServices] IDocumentDefinitionService documentDefinitionService, [FromServices] JsonSerializerOptions options)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var serializeEntity = JsonSerializer.Serialize(messageVariables.Data.GetProperty("entityData"));
            DocumentDefinitionInputDto entityData = JsonSerializer.Deserialize<DocumentDefinitionInputDto>(serializeEntity, options);

            documentDefinitionService.CreateDocumentDefinition(entityData, messageVariables.InstanceIdGuid);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult DefinitionUpdate([FromBody] dynamic body, [FromServices] IDocumentDefinitionService documentDefinitionService, [FromServices] JsonSerializerOptions options)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var serializeEntity = JsonSerializer.Serialize(messageVariables.Data.GetProperty("entityData"));
            DocumentDefinitionInputDto entityData = JsonSerializer.Deserialize<DocumentDefinitionInputDto>(serializeEntity, options);

            documentDefinitionService.UpdateDocumentDefinition(entityData, messageVariables.InstanceIdGuid);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult TimeoutDefinitionUpload([FromBody] dynamic body)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "TimeoutDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult DeleteProcessDefinitionUpload([FromBody] dynamic body)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "DeleteProcessDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult ErrorDefinitionUpload([FromBody] dynamic body)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "ErrorDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }
    }
}