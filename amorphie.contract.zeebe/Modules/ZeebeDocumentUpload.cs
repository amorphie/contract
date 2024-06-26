using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.zeebe.Model;
using Microsoft.AspNetCore.Mvc;
using amorphie.contract.zeebe.Services;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Services;
using Microsoft.OpenApi.Models;
using Dapr.Client;
using Newtonsoft.Json;

namespace amorphie.contract.zeebe.Modules
{
    public static class ZeebeDocumentUpload
    {

        public static void MapZeebeDocumentUploadEndpoints(this WebApplication app)
        {

            #region  map-post
            app.MapPost("/uploaded", Uploaded)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps uploaded service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentUpload) } };
                return operation;
            });
            app.MapPost("/autocontrol", AutoControl)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps AutoControl service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentUpload) } };

              return operation;
          });
            app.MapPost("/waitingcontrol", WaitingControl)
          .Produces(StatusCodes.Status200OK)
          .WithOpenApi(operation =>
          {
              operation.Summary = "Maps WaitingControl service worker on Zeebe";
              operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentUpload) } };

              return operation;
          });
            app.MapPost("/notvalidated", NotValidated)
                 .Produces(StatusCodes.Status200OK)
                 .WithOpenApi(operation =>
                 {
                     operation.Summary = "Maps NotValidated service worker on Zeebe";
                     operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentUpload) } };

                     return operation;
                 });
            app.MapPost("/validated2", Validated)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps Validated service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentUpload) } };

                return operation;
            });


            app.MapPost("/timeoutuploaded", TimeoutUploaded)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps Validated service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentUpload) } };

            return operation;
        });
            app.MapPost("/deleteprocessuploaded", DeleteProcessUploaded)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps DeleteProcessUploaded service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentUpload) } };

            return operation;
        });
            app.MapPost("/erroruploaded", ErrorUploaded)
        .Produces(StatusCodes.Status200OK)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Maps ErrorUploaded service worker on Zeebe";
            operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeDocumentUpload) } };

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
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);

            //todo: id yerine definition Code kullanıcaksın ön yuzu degiş
            dynamic? entityData = messageVariables.Data.GetProperty("entityData");
            var document = new amorphie.contract.core.Entity.Document.Document
            {
                Id = messageVariables.RecordIdGuid
            };

            var documentDefinitionIdString = entityData.GetProperty("document-definition-Id").ToString();
            var status = EStatus.OnHold;
            if (status != null)
                document.Status = status;

            Guid documentDefinitionId = ZeebeMessageHelper.StringToGuid(documentDefinitionIdString);


            var fileName = entityData.GetProperty("identity").ToString() + "_" + documentDefinitionIdString + "_" + entityData.GetProperty("file-name").ToString();
            document.DocumentContent = new DocumentContent
            {
                KiloBytesSize = entityData.GetProperty("file-size").ToString(),
                ContentType = entityData.GetProperty("file-type").ToString(),
                Name = fileName
            };
            var filebytes = ExtensionService.StringToBytes(entityData.GetProperty("file-byte-array").ToString(), entityData.GetProperty("file-size").ToString());


            _ = minioService.UploadFile(filebytes, fileName, entityData.GetProperty("file-type").ToString());

            document.DocumentDefinitionId = documentDefinitionId;//sonra
            var documentDefinition = dbContext.DocumentDefinition.FirstOrDefault(x => x.Id == documentDefinitionId);

            if (documentDefinition != null)
            {
                if (documentDefinition.DocumentOperations != null)
                    messageVariables.Variables.Add("IsAutoControl", !documentDefinition.DocumentOperations.DocumentManuelControl);
                else
                {
                    messageVariables.Variables.Add("IsAutoControl", false);
                }
            }
            else
            {
                messageVariables.Variables.Add("IsAutoControl", false);

            }
            dbContext.Document.Add(document);
            dbContext.SaveChanges();
            messageVariables.Variables.Add("documentDefinition", JsonConvert.SerializeObject(documentDefinition));

            messageVariables.Variables.Add("document", JsonConvert.SerializeObject(document));

            messageVariables.Success = true;

            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

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
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
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

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            // messageVariables.Variables.Remove("IsValidated");
            // messageVariables.Variables.Add("IsValidated", false);
            messageVariables.TransitionName = "waitingControl-upload-document";

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

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
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);

            core.Entity.Document.Document document = JsonConvert.DeserializeObject<amorphie.contract.core.Entity.Document.Document>(body.GetProperty("document").ToString());

            document.Status = EStatus.Passive;
            dbContext.Document.Update(document);
            dbContext.SaveChanges();
            messageVariables.Variables.Remove("document");
            messageVariables.Variables.Add("document", Newtonsoft.Json.JsonConvert.SerializeObject(document));

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

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

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            core.Entity.Document.Document document = JsonConvert.DeserializeObject<amorphie.contract.core.Entity.Document.Document>(body.GetProperty("document").ToString());

            document.Status = EStatus.Active;
            dbContext.Document.Update(document);
            dbContext.SaveChanges();
            messageVariables.Variables.Remove("document");
            messageVariables.Variables.Add("document", Newtonsoft.Json.JsonConvert.SerializeObject(document));

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

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

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "TimeoutUploaded";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

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

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "DeleteProcessUploaded";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));

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

            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            messageVariables.Success = true;
            messageVariables.LastTransition = "ErrorUploaded";
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));


        }
    }
}