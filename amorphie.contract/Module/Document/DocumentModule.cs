using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using amorphie.contract.application;
using amorphie.contract.core.Enum;
using Dapr;
using amorphie.contract.core.Model.Dys;
using amorphie.contract.core.Services;
using amorphie.contract.Extensions;
using amorphie.contract.core.Model.Colleteral;
using amorphie.contract.core.Response;
using amorphie.core.Base;

namespace amorphie.contract;

public class DocumentModule
    : BaseBBTRoute<RootDocumentDto, Document, ProjectDbContext>
{
    public DocumentModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "DocumentContentId" };

    public override string? UrlFragment => "document";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("search", getAllDocumentFullTextSearch);
        routeGroupBuilder.MapGet("getAll", getAllDocumentAll);
        routeGroupBuilder.MapPost("Instance", Instance);
        routeGroupBuilder.MapGet("download", DownloadDocument);
        routeGroupBuilder.MapPost("addDocumentToDys", AddDocumentToDys);
        routeGroupBuilder.MapPost("sendToTSIZL", SendToTSIZL);

    }

    async ValueTask<IResult> getAllDocumentFullTextSearch([FromServices] IDocumentAppService documentAppService, [AsParameters] PageComponentSearch dataSearch, CancellationToken cancellationToken)
    {
        var inputDto = new GetAllDocumentInputDto
        {
            Keyword = dataSearch.Keyword,
            Page = dataSearch.Page,
            PageSize = dataSearch.PageSize
        };

        var response = await documentAppService.GetAllDocumentFullTextSearch(inputDto, cancellationToken);

        // if (!response.Any())
        //     return Results.NoContent();

        return Results.Ok(response);
    }

    async ValueTask<IResult> getAllDocumentAll([FromServices] IDocumentAppService documentAppService, CancellationToken cancellationToken)
    {
        var response = await documentAppService.GetAllDocumentAll(cancellationToken);

        // if (!response.Any())
        //     return Results.NoContent();

        return Results.Ok(response);
    }

    async ValueTask<IResult> Instance([FromServices] IDocumentAppService documentAppService, HttpContext httpContext,
    CancellationToken token, [FromBody] DocumentInstanceInputDto input)
    {
        var headerModels = HeaderHelper.GetHeader(httpContext);
        input.SetHeaderParameters(headerModels.UserReference, headerModels.CustomerNo);
        var response = await documentAppService.Instance(input);

        return Results.Ok(new
        {
            Data = response,
            Success = true,
            ErrorMessage = "",
        });
    }

    async Task<GenericResult<DocumentDownloadOutputDto>> DownloadDocument([FromServices] IDocumentAppService documentAppService, HttpContext httpContext, [AsParameters] DocumentDownloadInputDto inputDto, CancellationToken token)
    {
        var headerModels = HeaderHelper.GetHeader(httpContext);
        inputDto.SetUserReference(headerModels.UserReference);

        var resDocument = await documentAppService.DownloadDocument(inputDto, token);

        return resDocument;
    }

    [Topic(KafkaConsts.KafkaName, KafkaConsts.SendDocumentInstanceDataToDYSTopicName)]
    [HttpPost]
    public async Task<GenericResult<bool>> AddDocumentToDys([FromBody] DocumentDysRequestModel documentDysRequestModel, [FromServices] IDysIntegrationService dysIntegrationService)
    {
        await dysIntegrationService.AddDysDocument(documentDysRequestModel);

        return GenericResult<bool>.Success(true);
    }

    [Topic(KafkaConsts.KafkaName, KafkaConsts.SendEngagementDataToTSIZLTopicName)]
    [HttpPost]
    public async Task<GenericResult<bool>> SendToTSIZL([FromBody] DoAutomaticEngagementPlainRequestDto requestModel, [FromServices] IColleteralIntegrationService colleteralIntegrationService, [FromServices] ICustomerIntegrationService customerIntegrationService)
    {
        var customerInfo = await customerIntegrationService.GetCustomerInfo(requestModel.AccountNumber);

        if (!customerInfo.IsSuccess)
        {
            return GenericResult<bool>.Fail(customerInfo.ErrorMessage);
        }

        requestModel.SetAccountBranchCode(customerInfo.Data.MainBranchCode);

        await colleteralIntegrationService.AddTSIZLDocument(requestModel);

        return GenericResult<bool>.Success(true);
    }

}






