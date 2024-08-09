using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using amorphie.contract.application;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Response;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Model;
using amorphie.contract.core.Model.Documents.Events;
using amorphie.contract.core.Services.Kafka;
using Dapr;

namespace amorphie.contract;

public class DocumentConsumerModule
    : BaseBBTRoute<RootDocumentDto, Document, ProjectDbContext>
{
    public DocumentConsumerModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "DocumentContentId" };

    public override string? UrlFragment => "document-events";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("create", DocumentCreate);
    }

    [Topic(KafkaConsts.KafkaName, KafkaConsts.DocumentCreateEventTopicName)]
    [HttpPost]
    public async Task<GenericResult<bool>> DocumentCreate([FromBody] KafkaData<DocumentCreateRequestEvent> requestDto,
                                                [FromServices] ProjectDbContext dbContext,
                                                [FromServices] IDocumentAppService documentAppService,
                                                [FromServices] IDocumentProducer documentProducer,
                                                [FromServices] ILogger<DocumentConsumerModule> logger,
                                                CancellationToken token)
    {

        var documentFailedEvent = MapToDocumentFailedEvent(requestDto.Data);

        if (IsRequestInvalid(requestDto, out string validationError))
        {
            return await PublishErrorEventAndReturnSuccess(documentFailedEvent, validationError, documentProducer);
        }

        var docUploadInputDto = new DocumentUploadInstanceInputDto
        {
            DocumentCode = requestDto.Data.DocumentCode,
            DocumentVersion = requestDto.Data.DocumentVersion,
            ContractCode = requestDto.Data.ContractCode,
            ContractInstanceId = requestDto.Data.ContractInstanceId,
            DocumentContent = new DocumentContentDto
            {
                ContentType = requestDto.Data.FileContentType,
                FileContext = Convert.ToBase64String(requestDto.Data.FileContent),
                FileName = requestDto.Data.FileName,
            }
        };

        docUploadInputDto.SetHeaderModel(new HeaderFilterModel(String.Empty, String.Empty, String.Empty, requestDto.Data.UserReference, requestDto.Data.CustomerNo));
        docUploadInputDto.SetDocumentInstanceId(requestDto.Data.DocumentInstanceId);

        var resultUpload = await documentAppService.DocumentUploadInstance(docUploadInputDto);

        if (!resultUpload.IsSuccess)
        {
            return await PublishErrorEventAndReturnSuccess(documentFailedEvent, $"Document upload error: {resultUpload.ErrorMessage}", documentProducer);
        }

        var approveInputDto = new ApproveDocumentInstanceInputDto
        {
            ContractCode = requestDto.Data.ContractCode,
            ContractInstanceId = requestDto.Data.ContractInstanceId,
            DocumentInstanceId = requestDto.Data.DocumentInstanceId
        };

        approveInputDto.SetHeaderModel(docUploadInputDto.HeaderModel);

        var resultApproval = await documentAppService.ApproveInstance(approveInputDto);
        if (!resultApproval.IsSuccess)
        {
            return await PublishErrorEventAndReturnSuccess(documentFailedEvent, $"Document auto-approval error: {resultUpload.ErrorMessage}", documentProducer);
        }

        var docContentId = await documentAppService.GetDocumentContentId(requestDto.Data.DocumentInstanceId);
        if (!docContentId.IsSuccess)
        {
            return await PublishErrorEventAndReturnSuccess(documentFailedEvent, $"Failed to fetch document content error: {resultUpload.ErrorMessage}", documentProducer);
        }

        await PublishCreatedEvent(docContentId.Data, requestDto.Data, documentProducer);

        return GenericResult<bool>.Success(true);
    }




    #region  Private Methods
    private DocumentCreationFailedEvent MapToDocumentFailedEvent(DocumentCreateRequestEvent requestDto)
    {
        return new DocumentCreationFailedEvent
        {
            Channel = requestDto.Channel,
            CustomerNo = requestDto.CustomerNo,
            DocumentInstanceId = requestDto.DocumentInstanceId,
            UserReference = requestDto.UserReference
        };
    }

    private bool IsRequestInvalid(KafkaData<DocumentCreateRequestEvent> requestDto, out string validationError)
    {
        validationError = string.Empty;

        if (requestDto.Data == null)
        {
            validationError = "Data cannot be null";
            return true;
        }

        if (!AppConsts.AllowedContentTypes.Contains(requestDto.Data.FileContentType) ||
            !FileExtension.CheckPdfFile(requestDto.Data.FileContent))
        {
            validationError = "Unsupported file type";
            return true;
        }

        return false;
    }

    private async Task<GenericResult<bool>> PublishErrorEventAndReturnSuccess(DocumentCreationFailedEvent documentFailedEvent,
                                                                             string errorMessage,
                                                                             IDocumentProducer documentProducer)
    {
        documentFailedEvent.ErrorMessage = errorMessage;

        await documentProducer.PublishDocumentCreationFailedEvent(documentFailedEvent);
        return GenericResult<bool>.Success(true);
    }

    private async Task PublishCreatedEvent(Guid documentContentId, DocumentCreateRequestEvent requestDto, IDocumentProducer documentProducer)
    {
        var createdEvent = new DocumentCreatedEvent
        {
            DocumentContentId = documentContentId,
            DocumentInstanceId = requestDto.DocumentInstanceId,
            Channel = requestDto.Channel,
            DocumentCode = requestDto.DocumentCode,
            DocumentVersion = requestDto.DocumentVersion,
            ContractCode = requestDto.ContractCode,
            ContractInstanceId = requestDto.ContractInstanceId,
            UserReference = requestDto.UserReference,
            CustomerNo = requestDto.CustomerNo,
        };

        await documentProducer.PublishDocumentCreatedEvent(createdEvent);
    }


    #endregion
}






