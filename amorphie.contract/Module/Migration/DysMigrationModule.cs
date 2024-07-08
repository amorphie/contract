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
using amorphie.contract.application.Migration;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Model;

namespace amorphie.contract;

public class DysMigrationModule
    : BaseBBTRoute<RootDocumentDto, Document, ProjectDbContext>
{
    public DysMigrationModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "DocumentDefinitionId", "DocumentContentId" };

    public override string? UrlFragment => "migration";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("dysDocument", MigrateDocument);
        // routeGroupBuilder.MapPost("sendToTSIZL", SendToTSIZL);
    }

    [Topic(KafkaConsts.KafkaName, KafkaConsts.DysDocumentJoined)]
    [HttpPost]
    public async Task<GenericResult<bool>> MigrateDocument([FromBody] DysDocumentInputDto dysDocumentInputDto, [FromServices] ProjectDbContext dbContext, [FromServices] IDocumentAppService documentAppService)
    {
        var s = dysDocumentInputDto;
        var d = s.ParseTagValue();

        var documentDefinition = await dbContext.DocumentDys.FirstOrDefaultAsync(k => k.ReferenceId == dysDocumentInputDto.TagId);

        if (documentDefinition is null)
        {
            // loglanacak
        }
        var headerModel = new HeaderFilterModel("X", "tr-TR", "", "userReference", 123);
        // ApproveDocumentInstanceInputDto approveInstance = new()
        // {
        //     ContractCode= "",
        //     ContractInstanceId = Guid.Empty,
        //     DocumentInstanceId 
        // };

        // await documentAppService.Instance()

        // await dysIntegrationService.AddDysDocument(documentDysRequestModel);

        return GenericResult<bool>.Success(true);
    }

    // [Topic(KafkaConsts.KafkaName, KafkaConsts.SendEngagementDataToTSIZLTopicName)]
    // [HttpPost]
    // public async Task<GenericResult<bool>> SendToTSIZL([FromBody] DoAutomaticEngagementPlainRequestDto requestModel, [FromServices] IColleteralIntegrationService colleteralIntegrationService, [FromServices] ICustomerIntegrationService customerIntegrationService)
    // {
    //     var customerInfo = await customerIntegrationService.GetCustomerInfo(requestModel.AccountNumber);

    //     if (!customerInfo.IsSuccess)
    //     {
    //         return GenericResult<bool>.Fail(customerInfo.ErrorMessage);
    //     }

    //     requestModel.SetAccountBranchCode(customerInfo.Data.MainBranchCode);

    //     await colleteralIntegrationService.AddTSIZLDocument(requestModel);

    //     return GenericResult<bool>.Success(true);
    // }

}






