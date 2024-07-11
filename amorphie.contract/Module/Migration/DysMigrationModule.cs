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
        // routeGroupBuilder.MapPost("dysDocument", MigrateDocument);
        routeGroupBuilder.MapPost("dysDocumentTag", MigrateDocumentTag);
    }

    // [Topic(KafkaConsts.KafkaName, KafkaConsts.DysDocument)]
    // [HttpPost]
    // public async Task<GenericResult<bool>> MigrateDocument([FromBody] KafkaData<DysDocumentKafkaInputDto> inputDto, [FromServices] ProjectDbContext dbContext)
    // {

    //     var migrationDysDocument = new DocumentMigrationDysDocument
    //     {
    //         Channel = inputDto.Message.Data.Channel,
    //         DocCreatedAt = inputDto.Message.Data.CreatedAt,
    //         DocId = inputDto.Message.Data.Id,
    //         Notes = inputDto.Message.Data.Notes,
    //         OwnerId = inputDto.Message.Data.OwnerId,
    //         Title = inputDto.Message.Data.Title,
    //         IsDeleted = Convert.ToBoolean(inputDto.Message.Data.IsDeleted),
    //     };

    //     var dysDoc = await dbContext.DocumentMigrationDysDocuments.FirstOrDefaultAsync(k => k.DocId == inputDto.Message.Data.Id);

    //     if (dysDoc is null)
    //     {
    //         await dbContext.DocumentMigrationDysDocuments.AddAsync(migrationDysDocument);
    //     }
    //     else
    //     {
    //         dysDoc.IsDeleted = Convert.ToBoolean(inputDto.Message.Data.IsDeleted);
    //         dysDoc.OwnerId = inputDto.Message.Data.OwnerId;
    //         dysDoc.Title = inputDto.Message.Data.Title;
    //         dysDoc.Notes = inputDto.Message.Data.Notes;
    //     }

    //     await dbContext.SaveChangesAsync();

    //     return GenericResult<bool>.Success(true);
    // }

    [Topic(KafkaConsts.KafkaName, KafkaConsts.DysDocumentTag)]
    [HttpPost]
    public async Task<GenericResult<bool>> MigrateDocumentTag([FromBody] KafkaData<DysDocumentTagKafkaInputDto> inputDto,
                                                [FromServices] ProjectDbContext dbContext,
                                                [FromServices] IDysMigrationAppService dysMigrationAppService)
    {

        var migrationDysDocumentTag = new DocumentMigrationDysDocumentTag
        {
            DocId = inputDto.Message.Data.DocId,
            TagId = inputDto.Message.Data.TagId,
            TagValues = inputDto.Message.Data.ParseTagValue(),
        };
        // migration başarılı ise DocumentMigrationDysDocumentTags tablosuna isDeleted = 1 yap;
        // eğer contract dan gönderilen dys kaydı geldiyse direkt geç. 

        var dysDocTag = await dbContext.DocumentMigrationDysDocumentTags.FirstOrDefaultAsync(k => k.DocId == inputDto.Message.Data.DocId);

        if (dysDocTag is null)
        {
            await dbContext.DocumentMigrationDysDocumentTags.AddAsync(migrationDysDocumentTag);
        }
        else
        {
            dysDocTag.TagValues = inputDto.Message.Data.ParseTagValue();
        }

        await dbContext.SaveChangesAsync();

        if (inputDto.Message.Data.IsAllowedTagId())
        {
            // RUN migration

            var docMigrationProcessing = new DocumentMigrationProcessing
            {
                Status = AppConsts.NotStarted,
                TagId = inputDto.Message.Data.TagId,
                DocId = inputDto.Message.Data.DocId,
                LastTryTime = DateTime.UtcNow,
            };

            docMigrationProcessing.IncreaseTryCount();

            try
            {
                var result = await dysMigrationAppService.RunMigrationWorker(inputDto.Message.Data);


                if (result.IsSuccess)
                {
                    dysDocTag.IsDeleted = true;
                
                    docMigrationProcessing.ChangeStatus(AppConsts.Completed);
                }
                else
                {
                    docMigrationProcessing.ChangeStatus(AppConsts.Failed, result.ErrorMessage);
                }

                await dbContext.DocumentMigrationProcessings.AddAsync(docMigrationProcessing);
                
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                docMigrationProcessing.ChangeStatus(AppConsts.Failed, ex.Message);
                await dbContext.DocumentMigrationProcessings.AddAsync(docMigrationProcessing);
                await dbContext.SaveChangesAsync();
                throw;
            }


            // await dbContext.DocumentMigrationProcessings.AddAsync(new DocumentMigrationProcessing
            // {
            //     DocId = inputDto.Message.Data.DocId,
            //     Status = AppConsts.NotStarted,
            //     TagId = inputDto.Message.Data.TagId,
            // });
        }



        return GenericResult<bool>.Success(true);
    }



}






