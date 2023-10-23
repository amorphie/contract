using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.zeebe.Service.Minio;
using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Model;
using Dapr.Client;
using Google.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using Google.Protobuf;
using amorphie.contract.zeebe.Services;
using amorphie.contract.zeebe.Services.Interfaces;

namespace amorphie.contract.zeebe.Modules
{
    public static class ZeebeDocumentUpload
    {

        public static void MapZeebeDocumentUploadEndpoints(this WebApplication app)
        {
            #region auto-map
            //  var  mapList = new List<string>{"Uploaded","AutoControl","WaitingControl",
            // "NotValidated","Validated","TimeoutUploaded","DeleteProcessUploaded","ErrorUploaded"};
            // foreach (var item in mapList)
            // {
            //     app.MapPost("/"+item,new Delegate)
            //                 .Produces(StatusCodes.Status200OK)
            //                 .WithOpenApi(operation =>
            //                 {
            //                     operation.Summary = string.Format("_summary",item);
            //                     operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe Contract Document Upload" } };
            //                     return operation;
            //                 });
            // }
            #endregion
            #region  map-post
            app.MapPost("/uploaded", Uploaded)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps uploaded service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe Contract Document Upload" } };
                return operation;
            });
            app.MapPost("/autocontrol", AutoControl)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps AutoControl service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe Contract Document Upload" } };
              return operation;
          });
            app.MapPost("/WaitingControl", WaitingControl)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps WaitingControl service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe Contract Document Upload" } };
              return operation;
          });
            app.MapPost("/NotValidated", NotValidated)
                 .Produces(StatusCodes.Status200OK)
                 .WithOpenApi(operation =>
                 {
                     operation.Summary = "Maps NotValidated service worker on Zeebe";
                     operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe Contract Document Upload" } };
                     return operation;
                 });
            app.MapPost("/Validated", Validated)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps Validated service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe Contract Document Upload" } };
                return operation;
            });


            app.MapPost("/TimeoutUploaded", TimeoutUploaded)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps Validated service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe Contract Document Upload" } };
            return operation;
        });
            app.MapPost("/DeleteProcessUploaded", DeleteProcessUploaded)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps DeleteProcessUploaded service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe Contract Document Upload" } };
            return operation;
        });
            app.MapPost("/ErrorUploaded", ErrorUploaded)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps ErrorUploaded service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Zeebe Contract Document Upload" } };
            return operation;
        });
            #endregion
        }
        static IResult Uploaded(
          [FromBody] dynamic body,
         [FromServices] ProjectDbContext dbContext,
          HttpRequest request,
          HttpContext httpContext,
          [FromServices] DaprClient client
          , IConfiguration configuration,
          [FromServices] IMinioService minioService
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
                var document = new amorphie.contract.core.Entity.Document.Document();
                var documentDefinitionIdString = entityData.GetProperty("document-definition-Id").ToString();

                Guid documentDefinitionId;
                if (!Guid.TryParse(documentDefinitionIdString, out documentDefinitionId))
                {
                    throw new Exception("DocumentDefinitionId not provided or not as a GUID");
                }

                var fileName = entityData.GetProperty("identity").ToString() + "_" + documentDefinitionIdString + "_" + entityData.GetProperty("file-name").ToString();
                document.DocumentContent = new DocumentContent
                {
                    KiloBytesSize = entityData.GetProperty("file-size").ToString(),
                    ContentType = entityData.GetProperty("file-type").ToString(),
                    Name = fileName,
                    ContentData = entityData.GetProperty("file-byte-array").ToString()
                };
                var filebytes = ExtensionService.StringToBytes(entityData.GetProperty("file-byte-array").ToString(), entityData.GetProperty("file-size").ToString());


                _ = minioService.UploadFile(filebytes, fileName, entityData.GetProperty("file-type").ToString());

                document.DocumentDefinitionId = documentDefinitionId;
                var documentDefinition = dbContext.DocumentDefinition.FirstOrDefault(x => x.Id == documentDefinitionId);

                if (documentDefinition != null)
                {
                    messageVariables.Variables.Add("documentDefinition", Newtonsoft.Json.JsonConvert.SerializeObject(documentDefinition));
                    messageVariables.Variables.Add("IsAutoControl", documentDefinition.DocumentOperations.DocumentManuelControl);
                }else{
                    messageVariables.Variables.Add("IsAutoControl", false);

                }

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

                messageVariables.Variables.Remove("IsValidated");
                messageVariables.Variables.Add("IsValidated", false);
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
                // dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                // string reference = entityData.GetProperty("reference").ToString();
                // string deviceId = entityData.GetProperty("deviceId").ToString();
                messageVariables.Variables.Remove("IsValidated");
                messageVariables.Variables.Add("IsValidated", false);
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
                messageVariables.Variables.Remove("IsValidated");
                messageVariables.Variables.Add("IsValidated", true);
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
                // dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                // string reference = entityData.GetProperty("reference").ToString();
                // string deviceId = entityData.GetProperty("deviceId").ToString();
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
                // dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                // string reference = entityData.GetProperty("reference").ToString();
                // string deviceId = entityData.GetProperty("deviceId").ToString();
                messageVariables.Success = true;
                messageVariables.LastTransition = "TimeoutUploaded";
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "TimeoutUploaded";

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
                // dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                // string reference = entityData.GetProperty("reference").ToString();
                // string deviceId = entityData.GetProperty("deviceId").ToString();
                messageVariables.Success = true;
                messageVariables.LastTransition = "DeleteProcessUploaded";
                return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
            }

            catch (Exception ex)
            {
                messageVariables.Success = true;
                messageVariables.Message = ex.Message;
                messageVariables.LastTransition = "DeleteProcessUploaded";

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
                // dynamic? entityData = messageVariables.Data.GetProperty("entityData");
                // string reference = entityData.GetProperty("reference").ToString();
                // string deviceId = entityData.GetProperty("deviceId").ToString();
                messageVariables.Success = true;
                messageVariables.LastTransition = "ErrorUploaded";
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