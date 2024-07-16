using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using amorphie.contract.application;
using amorphie.contract.core.Enum;
using Dapr;
using amorphie.contract.core.Response;
using amorphie.contract.application.Migration;
using Microsoft.EntityFrameworkCore;

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
        routeGroupBuilder.MapPost("dysDocumentTag", MigrateDocumentTag);
    }

    [Topic(KafkaConsts.KafkaName, KafkaConsts.DysDocumentTag)]
    [HttpPost]
    public async Task<GenericResult<bool>> MigrateDocumentTag([FromBody] KafkaData<DysDocumentTagKafkaInputDto> inputDto,
                                                [FromServices] ProjectDbContext dbContext,
                                                [FromServices] IDysMigrationAppService dysMigrationAppService)
    {

        var dysDocTag = await dbContext.DocumentMigrationDysDocumentTags.FirstOrDefaultAsync(k => k.DocId == inputDto.Message.Data.DocId);

        if (dysDocTag is null)
        {
            await dbContext.DocumentMigrationDysDocumentTags.AddAsync(new DocumentMigrationDysDocumentTag
            {
                DocId = inputDto.Message.Data.DocId,
                TagId = inputDto.Message.Data.TagId,
                TagValues = inputDto.Message.Data.ParseTagValue(),
            });
        }
        else
        {
            dysDocTag.TagValues = inputDto.Message.Data.ParseTagValue();
        }

        await dbContext.SaveChangesAsync();

        if (inputDto.Message.Data.IsAllowedTagId())
        {
            // RUN migration
            var docMigrationProcessing = await dbContext.DocumentMigrationProcessings
                    .FirstOrDefaultAsync(k => k.DocId == inputDto.Message.Data.DocId && k.TagId == inputDto.Message.Data.TagId);

            if (docMigrationProcessing is null)
            {
                docMigrationProcessing = new DocumentMigrationProcessing
                {
                    Status = AppConsts.NotStarted,
                    TagId = inputDto.Message.Data.TagId,
                    DocId = inputDto.Message.Data.DocId,
                    LastTryTime = DateTime.UtcNow,
                };

                await dbContext.DocumentMigrationProcessings.AddAsync(docMigrationProcessing);
            }
            else
                docMigrationProcessing.IncreaseTryCount();

            try
            {
                var result = await dysMigrationAppService.RunMigrationWorker(inputDto.Message.Data);

                if (result.IsSuccess)
                {
                    dysDocTag.IsDeleted = true;

                    docMigrationProcessing.ChangeStatus(result.Data.Status);
                }
                else
                {
                    docMigrationProcessing.ChangeStatus(AppConsts.Failed, result.ErrorMessage);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                docMigrationProcessing.ChangeStatus(AppConsts.Failed, ex.Message);
                await dbContext.SaveChangesAsync();
                throw;
            }
        }



        return GenericResult<bool>.Success(true);
    }
}






