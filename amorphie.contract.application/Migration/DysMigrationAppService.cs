using amorphie.contract.application.ConverterFactory;
using amorphie.contract.application.Customer;
using amorphie.contract.application.Customer.Dto;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Model.Documents;
using amorphie.contract.core.Response;
using amorphie.contract.core.Services;
using amorphie.contract.infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace amorphie.contract.application.Migration;

public interface IDysMigrationAppService
{
    Task<GenericResult<bool>> RunMigrationWorker(DysDocumentTagKafkaInputDto inputDto);
}

public class DysMigrationAppService : IDysMigrationAppService
{

    private readonly ProjectDbContext _dbContext;
    private readonly ILogger _logger;

    private readonly IDysIntegrationService _dysIntegrationService;
    private readonly ICustomerIntegrationService _customerIntegrationService;
    private readonly ICustomerAppService _customerAppService;
    private readonly IDocumentAppService _documentAppService;

    private readonly FileConverterFactory _fileConverterFactory;

    public DysMigrationAppService(ProjectDbContext projectDbContext,
    ILogger logger,
    IDysIntegrationService dysIntegrationService,
    ICustomerIntegrationService customerIntegrationService,
    ICustomerAppService customerAppService,
    IDocumentAppService documentAppService,
    FileConverterFactory fileConverterFactory)
    {
        _dbContext = projectDbContext;
        _logger = logger;
        _dysIntegrationService = dysIntegrationService;
        _customerIntegrationService = customerIntegrationService;
        _customerAppService = customerAppService;
        _documentAppService = documentAppService;
        _fileConverterFactory = fileConverterFactory;
    }

    public async Task<GenericResult<bool>> RunMigrationWorker(DysDocumentTagKafkaInputDto inputDto)
    {

        using var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            int tagId = Convert.ToInt32(inputDto.TagId);

            // var documentDys = await _dbContext.DocumentDys
            //                                     .Include(k => k.DocumentDefinition)
            //                                     .AsNoTracking()
            //                                     .FirstOrDefaultAsync(k => k.ReferenceId == tagId);

            var documentDys = await (from c in _dbContext.DocumentDys
                                     join d in
                                                   _dbContext.DocumentDefinition on c.DocumentDefinitionId equals d.Id
                                     where c.ReferenceId == tagId
                                     select new
                                     {
                                         d.Code,
                                         d.Semver,
                                         d.Id
                                     }).FirstOrDefaultAsync();

            if (documentDys is null)
            {
                string warMessage = $"Dys TagID {tagId} için doküman tanımı bulunamadı.";
                _logger.Warning(warMessage);
                return GenericResult<bool>.Fail(warMessage);
            }

            // await _dbContext.DocumentMigrationAggregations.AddAsync(new DocumentMigrationAggregation
            // {
            //     ContractCodes = new[] { new DocumentMigrationContractModel { Code = "cont-enes-16042024" } },
            //     DocumentCode = documentDys.Code,
            // });

            // await _dbContext.SaveChangesAsync();

            var checkDocumentForContract = await _dbContext.DocumentMigrationAggregations
                                                    .AsNoTracking()
                                                    .FirstOrDefaultAsync(k => k.DocumentCode == documentDys.Code);
            if (checkDocumentForContract is null)
            {
                string warMessage = $"Dys TagID: {tagId} ve Code:{documentDys.Code} için doküman aggregations tanımı bulunamadı.";
                _logger.Warning(warMessage);
                return GenericResult<bool>.Fail(warMessage);
            }

            var dmsDocument = await _dysIntegrationService.GetDocumentAndData(inputDto.DocId);
            long customerNo = Convert.ToInt64(dmsDocument.Data.DocumentModel.CustomerNo);


            var customerDMSInfo = await _customerIntegrationService.GetCustomerInfo(customerNo);

            if (!customerDMSInfo.IsSuccess || customerDMSInfo is null)
            {
                _logger.Error("CustomerInfo servisinden kayıt çekilemedi. {customerNo}", customerNo);
                return GenericResult<bool>.Fail(customerDMSInfo.ErrorMessage);
            }

            var userReference = customerDMSInfo.Data.GetReference();

            var customerInputDto = new CustomerInputDto(userReference, userReference, customerNo, customerDMSInfo.Data.TaxNo);
            var customerId = await _customerAppService.UpsertAsync(customerInputDto);

            var instanceMetadata = new List<MetadataDto>();

            foreach (var item in inputDto.ParseTagValue())
            {
                instanceMetadata.Add(new MetadataDto
                {
                    Code = item.Key,
                    Data = item.Value,
                    Title = item.Key
                });
            }

            string fileBase64 = Convert.ToBase64String(dmsDocument.Data.DocumentFile.FileContent);
            string fileMimeType = dmsDocument.Data.DocumentFile.MimeType;
            DocumentContentDto? documentContentOrgFile = null;

            // File checker and converter 
            if (!AppConsts.AllowedContentTypes.Contains(fileMimeType))
            {
                documentContentOrgFile = new DocumentContentDto
                {
                    ContentType = dmsDocument.Data.DocumentFile.MimeType,
                    FileContext = fileBase64,
                    FileName = dmsDocument.Data.DocumentFile.FileName
                };

                var converter = _fileConverterFactory.GetConverter(dmsDocument.Data.DocumentFile.MimeType);
                var fl = await converter.GetFileContentAsync(fileBase64);
                fileBase64 = Convert.ToBase64String(fl);
                fileMimeType = FileExtension.Pdf;
            }

            var migrateDocumentInput = new MigrateDocumentInputDto
            {
                UserReference = userReference,
                CustomerId = customerId.Data,
                DocumentCode = documentDys.Code,
                DocumentDefinitionId = documentDys.Id,
                DocumentMigrationContracts = checkDocumentForContract.ContractCodes.ToList(),
                DocumentVersion = documentDys.Semver,
                InstanceMetadata = instanceMetadata,
                Notes = [
                    new NoteDto{
                    Note = dmsDocument.Data.DocumentModel.Notes
                }
                ],
                DocumentContent = new DocumentContentDto
                {
                    ContentType = fileMimeType,
                    FileContext = fileBase64,
                    FileName = dmsDocument.Data.DocumentFile.FileName
                },
                DocumentContentOriginal = documentContentOrgFile
            };

            var result = await _documentAppService.MigrateDocument(migrateDocumentInput);

            if (result.IsSuccess)
                await transaction.CommitAsync();
            else
                transaction.Rollback();

            return result;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }


}