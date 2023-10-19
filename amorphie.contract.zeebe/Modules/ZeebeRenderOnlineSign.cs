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
    public static class ZeebeRenderOnlineSign
    {
        public static void MapZeebeRenderOnlineSignEndpoints(this WebApplication app)
        {
            app.MapPost("/Render", Render)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps Render service worker on Zeebe";
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


            app.MapPost("/TimeoutRenderOnlineSign", TimeoutRenderOnlineSign)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps TimeoutRenderOnlineSign service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
            return operation;
        });
            app.MapPost("/DeleteRenderOnlineSign", DeleteRenderOnlineSign)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps DeleteRenderOnlineSign service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe" } };
            return operation;
        });
            app.MapPost("/ErrorRenderOnlineSign", ErrorRenderOnlineSign)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps ErrorRenderOnlineSign service worker on Zeebe";
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


        static IResult TimeoutRenderOnlineSign(
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
        static IResult DeleteRenderOnlineSign(
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
        static IResult ErrorRenderOnlineSign(
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