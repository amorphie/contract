using System.Text.Json;
using amorphie.contract.application;
using amorphie.contract.zeebe.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace amorphie.contract.zeebe.Modules.ZeebeDocumentDef
{
    public static class ZeebeDocumentGroupDefinition
    {
        public static void MapZeebeDocumentGroupDefinitionEndpoints(this WebApplication app)
        {
            app.MapPost("/create-document-group-definition", CreateDocumentGroupDefinition)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps documentgroupdefinition service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentGroupDefinition) } };
                return operation;
            });

            app.MapPost("/updated-document-group-definition", UpdateDocumentGroupDefinition)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps documentgroupdefinition service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentGroupDefinition) } };
                return operation;
            });

            app.MapPost("/error-document-group-definition", ErrorDocumentGroupDefinition)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps errordocumentgroupdefinition service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentGroupDefinition) } };

                return operation;
            });

            app.MapPost("/delete-document-group-definition", DeleteDocumentGroupDefinition)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps deletedocumentgroupdefinition service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentGroupDefinition) } };

                return operation;
            });

            app.MapPost("/timeout-document-group-definition", TimeoutDocumentGroupDefinition)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps timeoutdocumentgroupdefinition service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentGroupDefinition) } };

                return operation;
            });
        }

        static IResult CreateDocumentGroupDefinition([FromBody] dynamic body, [FromServices] IDocumentGroupDefinitionService documentGroupDefinitionService, [FromServices] JsonSerializerOptions options)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var serializeEntity = JsonSerializer.Serialize(messageVariables.Data.GetProperty("entityData"));
            DocumentGroupInputDto entityData = JsonSerializer.Deserialize<DocumentGroupInputDto>(serializeEntity, options);

            documentGroupDefinitionService.CreateDocumentGroup(entityData, messageVariables.RecordIdGuid);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult UpdateDocumentGroupDefinition([FromBody] dynamic body, [FromServices] IDocumentGroupDefinitionService documentGroupDefinitionService, [FromServices] JsonSerializerOptions options)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var serializeEntity = JsonSerializer.Serialize(messageVariables.Data.GetProperty("entityData"));
            DocumentGroupInputDto entityData = JsonSerializer.Deserialize<DocumentGroupInputDto>(serializeEntity, options);

            documentGroupDefinitionService.UpdateDocumentGroup(entityData, messageVariables.RecordIdGuid);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult TimeoutDocumentGroupDefinition([FromBody] dynamic body)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "TimeoutDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult DeleteDocumentGroupDefinition([FromBody] dynamic body)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "DeleteProcessDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult ErrorDocumentGroupDefinition([FromBody] dynamic body)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "ErrorDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }
    }
}