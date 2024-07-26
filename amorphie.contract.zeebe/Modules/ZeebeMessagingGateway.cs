using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.application.MessagingGateway.Dto;
using amorphie.contract.core.Model;
using amorphie.contract.zeebe.Extensions.HeaderHelperZeebe;
using amorphie.contract.zeebe.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace amorphie.contract.zeebe.Modules
{
    public static class ZeebeMessagingGateway
    {
        public static void MapZeebeContractMailEndpoints(this WebApplication app)
        {
            app.MapPost("/get-document-codes-by-contract-code-for-email", GetDocumentCodesByContractCodeForEmail)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps sendtemplatedmail service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeMessagingGateway) } };
                return operation;
            });

            app.MapPost("/send-contract-documents-mail", SendContractDocumentsMail)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Maps sendtemplatedmail service worker on Zeebe";
                operation.Tags = new List<OpenApiTag> { new() { Name = nameof(ZeebeMessagingGateway) } };
                return operation;
            });
        }

        static async ValueTask<IResult> GetDocumentCodesByContractCodeForEmail([FromBody] dynamic body, [FromServices] IMessagingGatewayService messagingGatewayService, [FromServices] JsonSerializerOptions options)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var headerModel = HeaderHelperZeebe.GetHeader(body) as HeaderFilterModel;
            
            var inputDto = ZeebeMessageHelper.MapToDto<FindMailAttachmentDto>(body) as FindMailAttachmentDto;
            inputDto.SetHeaderModel(headerModel);

            var response = await messagingGatewayService.FindMailAttachmentCodes(inputDto);

            messageVariables.Variables.Add(ZeebeConsts.FindMailAttachmentOutputDto, response);

            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }

        static IResult SendContractDocumentsMail([FromBody] dynamic body, [FromServices] IMessagingGatewayService messagingGatewayService, [FromServices] JsonSerializerOptions options)
        {
            var messageVariables = ZeebeMessageHelper.VariablesControl(body);
            var serializeEntity = JsonSerializer.Serialize(messageVariables.Data.GetProperty("entityData"));
            
            
            var headerModel = HeaderHelperZeebe.GetHeader(body) as HeaderFilterModel;
            
            var inputDto = ZeebeMessageHelper.MapToDto<SendContractDocumentsMailInputDto>(body) as SendContractDocumentsMailInputDto;
            inputDto.SetHeaderModel(headerModel);

            SendDocumentsToCustomerInputDto sendDocumentsDto = new SendDocumentsToCustomerInputDto
            {
                Sender = inputDto.HeaderModel.EBankEntity == core.Enum.EBankEntity.on ? "On" : "Burgan",
                TCKN = inputDto.HeaderModel.UserReference,
                RelatedInstanceId = inputDto.RelatedInstanceId,
                DocumentCodes = inputDto.DocumentCodes
            };

            var isSuccess = messagingGatewayService.SendDocumentsToCustomer(sendDocumentsDto);

            messageVariables.Variables.Add(ZeebeConsts.SendContractDocumentsMailOutputDto, isSuccess);
            messageVariables.Success = true;
            return Results.Ok(ZeebeMessageHelper.CreateMessageVariables(messageVariables));
        }
    }
}