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
using amorphie.contract.core.Services.Kafka;

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
        routeGroupBuilder.MapGet("runWorker", MigrateDocumentByTagId);
    }

    [Topic(KafkaConsts.KafkaName, KafkaConsts.DysDocumentTag)]
    [HttpPost]
    public async Task<GenericResult<bool>> MigrateDocumentTag([FromBody] KafkaData<DysDocumentTagKafkaInputDto> inputDto,
                                               [FromServices] ProjectDbContext dbContext,
                                               [FromServices] IDysMigrationAppService dysMigrationAppService)
    {

        if (inputDto.Data is not null)
        {
            // dapr tarafından kendimiz publish ettiğimizde farklı formatta geldiği için message tekrar dolduruldu.
            inputDto.Message.Data = inputDto.Data;
        }

        var dysDocTag = await UpsertDocumentMigrationDysDocTag(dbContext, inputDto.Message.Data);

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
            else if (!String.IsNullOrEmpty(docMigrationProcessing.ErrorMessage) || docMigrationProcessing.IsTimeout())
            {
                // Qlik replicate den UPDATE işlemi geliyorsa bunu tryCount olarak saydırma. Try count sadece hata alınıp tekrar denendiğinde sayılmalı.
                docMigrationProcessing.IncreaseTryCount();
            }

            await dbContext.SaveChangesAsync();
            
            try
            {
                if (docMigrationProcessing.IsExceededMaxRetryCount())
                {
                    docMigrationProcessing.ChangeStatus(AppConsts.Abandoned);
                }
                else
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
                }

                await dbContext.SaveChangesAsync();
                return GenericResult<bool>.Success(true);
            }
            catch (NotSupportedException fileSupportedEx)
            {
                docMigrationProcessing.ChangeStatus(AppConsts.Failed, fileSupportedEx.Message);
                await dbContext.SaveChangesAsync();
                // desteklenmeyen bir dosya türü varsa tekrar deneme yapmaya gerek yok.
                return GenericResult<bool>.Success(true);
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

    public async ValueTask<GenericResult<bool>> MigrateDocumentByTagId([
            FromServices] ProjectDbContext dbContext,
            [FromServices] IDysProducer dysProducer,
            CancellationToken token,
            string tagId)
    {

        var dysDocuments = await dbContext.DocumentMigrationDysDocumentTags
                                    .Where(k => k.TagId == tagId).ToListAsync(token);


        foreach (var item in dysDocuments)
        {

            var docDto = new DysDocumentTagKafkaInputDto
            {
                DocId = item.DocId,
                TagId = item.TagId,
                TagValue = item.TagValuesOrg,
                IncludeAllowedTagId = tagId
            };

            await dysProducer.PublishDysDataAgainToContract(docDto);
        }

        return GenericResult<bool>.Success(true);

    }


    #region  Private Methods
    private async Task<DocumentMigrationDysDocumentTag> UpsertDocumentMigrationDysDocTag(ProjectDbContext dbContext, DysDocumentTagKafkaInputDto inputDto)
    {
        var dysDocTag = await dbContext.DocumentMigrationDysDocumentTags
                                .IgnoreQueryFilters()
                                .FirstOrDefaultAsync(k => k.DocId == inputDto.DocId);

        if (dysDocTag is null)
        {
            dysDocTag = new DocumentMigrationDysDocumentTag
            {
                DocId = inputDto.DocId,
                TagId = inputDto.TagId,
                TagValues = inputDto.ParseTagValue(),
                TagValuesOrg = inputDto.TagValue
            };

            await dbContext.DocumentMigrationDysDocumentTags.AddAsync(dysDocTag);
        }
        else
        {
            dysDocTag.IsDeleted = false;
            dysDocTag.TagValues = inputDto.ParseTagValue();
            dysDocTag.TagValuesOrg = inputDto.TagValue;
        }

        await dbContext.SaveChangesAsync();
        return dysDocTag;
    }

    #endregion
}






