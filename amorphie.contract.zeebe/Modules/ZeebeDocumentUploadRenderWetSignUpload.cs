using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Model;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace amorphie.contract.zeebe.Modules
{
    public static class ZeebeDocumentUploadRenderWetSignUpload
    {
        public static void MapZeebeDocumentUploadRenderWetSignUploadEndpoints(this WebApplication app)
        {
            app.MapPost("/render", Render)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps Render service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
                return operation;
            });
            app.MapPost("/amorphie-workflow-set-state", AutoControl)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps AutoControl service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
              return operation;
          });
            app.MapPost("/WaitingControl", WaitingControl)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps WaitingControl service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
              return operation;
          });
            app.MapPost("/NotValidated", NotValidated)
                 .Produces(StatusCodes.Status200OK)
                 .WithOpenApi(operation =>
                 {
                     operation.Summary = "Maps NotValidated service worker on Zeebe";
                     operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
                     return operation;
                 });
            app.MapPost("/Validated", Validated)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps Validated service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
                return operation;
            });


            app.MapPost("/TimeoutUploaded", TimeoutUploaded)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps Validated service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
            return operation;
        });
            app.MapPost("/DeleteProcessUploaded", DeleteProcessUploaded)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps Validated service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
            return operation;
        });
            app.MapPost("/ErrorUploaded", ErrorUploaded)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps Validated service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
            return operation;
        });
        }
        static IResult Render(
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
                messageVariables.LastTransition = "Buraya bir sonraki";
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
        static IResult AutoControl(
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
                messageVariables.LastTransition = "Buraya bir sonraki";
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
        static IResult WaitingControl(
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
                messageVariables.LastTransition = "Buraya bir sonraki";
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
        static IResult NotValidated(
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
                messageVariables.LastTransition = "Buraya bir sonraki";
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
        static IResult Validated(
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
                messageVariables.LastTransition = "Buraya bir sonraki";
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


          static IResult TimeoutUploaded(
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
                messageVariables.LastTransition = "Buraya bir sonraki";
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
          static IResult DeleteProcessUploaded(
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
                messageVariables.LastTransition = "Buraya bir sonraki";
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
          static IResult ErrorUploaded(
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
                messageVariables.LastTransition = "Buraya bir sonraki";
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
    }
}