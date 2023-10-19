using System.Runtime.CompilerServices;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Model;
using amorphie.contract.zeebe.Model.DocumentDefinitionDataModel;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.zeebe.Services.Interfaces;

namespace amorphie.contract.zeebe.Modules.ZeebeDocumentDef
{
    public static class ZeebeDocumentDefinition
    {
        public static void MapZeebeDocumentDefinitionEndpoints(this WebApplication app)
        {
            app.MapPost("/DefinitionUpload", DefinitionUpload)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps Render service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
                return operation;
            });

            app.MapPost("/ErrorDefinitionUpload", ErrorDefinitionUpload)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps Render service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
              return operation;
          });
            app.MapPost("/DeleteProcessDefinitionUpload", DeleteProcessDefinitionUpload)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps Render service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
              return operation;
          });
            app.MapPost("/TimeoutDefinitionUpload", TimeoutDefinitionUpload)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps Render service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
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

                dynamic? entityData = messageVariables.Data.GetProperty("entityData").ToString();
                var data = JsonConvert.DeserializeObject<DocumentDefinitionDataModel>(entityData);
                DocumentDefinitionDataModel documentDefinitionDataModel;
                if (data is DocumentDefinitionDataModel)
                {
                    documentDefinitionDataModel = data;

                }
                else
                {
                    throw new Exception("DefinitionUpload data is DocumentDefinitionDataModel");
                }
              
                messageVariables.Success = true;
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "ErrorDefinition";

                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }
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
        static IResult DeleteProcessDefinitionUpload(
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
        static IResult ErrorDefinitionUpload(
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