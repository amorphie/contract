using System;
using amorphie.contract.application.Contract.Dto.Input;
using amorphie.contract.zeebe.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.zeebe.Modules.ZeebeDocumentDef;
using Microsoft.OpenApi.Models;

namespace amorphie.contract.zeebe.Modules.ZeebeDefinitions
{
    public static class ZeebeContractCategoryDefinition
    {
        public static void MapZeebeContractCategoryDefinitionEndpoints(this WebApplication app)
        {
            app.MapPost("/contractcategorydefinitionupdate", ContractCategoryDefinitionUpdate)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps contractcategorydefinition service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };
                return operation;
            });

            app.MapPost("/contractcategorydefinition", ContractCategoryDefinition)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps contractcategorydefinition service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };
                return operation;
            });

            app.MapPost("/contractcategorydetailadd", ContractCategoryDetailAdd)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps contractcategorydetailadd service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };
                return operation;
            });

            app.MapPost("/errorcontractcategorydefinition", ErrorContractCategoryDefinition)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps errorcontractcategorydefinition service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };

              return operation;
          });
            app.MapPost("/deletecontractcategorydefinition", DeleteContractCategoryDefinition)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps deletecontractcategorydefinition service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };

              return operation;
          });
            app.MapPost("/timeoutcontractcategorydefinition", TimeoutContractCategoryDefinition)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps Render service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeContractDefinition) } };

              return operation;
          });

        }

        static IResult ContractCategoryDefinition([FromBody] dynamic body, [FromServices] IContractCategoryAppService contractCategoryAppService, [FromServices] JsonSerializerOptions options)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var serializeEntity = JsonSerializer.Serialize(messageVariables.Data.GetProperty("entityData"));
            ContractCategoryDto entityData = JsonSerializer.Deserialize<ContractCategoryDto>(serializeEntity, options);

            contractCategoryAppService.CreateContractCategory(entityData, messageVariables.InstanceIdGuid);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult ContractCategoryDefinitionUpdate([FromBody] dynamic body, [FromServices] IContractCategoryAppService contractCategoryAppService, [FromServices] JsonSerializerOptions options)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var serializeEntity = JsonSerializer.Serialize(messageVariables.Data.GetProperty("entityData"));
            ContractCategoryDto entityData = JsonSerializer.Deserialize<ContractCategoryDto>(serializeEntity, options);

            contractCategoryAppService.UpdateContractCategory(entityData, messageVariables.InstanceIdGuid);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult ContractCategoryDetailAdd([FromBody] dynamic body, [FromServices] IContractCategoryAppService contractCategoryAppService, [FromServices] JsonSerializerOptions options)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var serializeEntity = JsonSerializer.Serialize(messageVariables.Data.GetProperty("entityData"));
            ContractCategoryDetailInputDto entityData = JsonSerializer.Deserialize<ContractCategoryDetailInputDto>(serializeEntity, options);

            contractCategoryAppService.ContractCategoryAdd(entityData);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult TimeoutContractCategoryDefinition([FromBody] dynamic body)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "TimeoutDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult DeleteContractCategoryDefinition([FromBody] dynamic body)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "DeleteProcessDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult ErrorContractCategoryDefinition([FromBody] dynamic body)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "ErrorDefinitionUpload";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }
    }
}

