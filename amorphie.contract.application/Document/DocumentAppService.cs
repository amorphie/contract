using System.Data;
using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.ConverterFactory;
using amorphie.contract.application.Customer;
using amorphie.contract.application.Customer.Dto;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.core;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;
using amorphie.contract.core.Model.Colleteral;
using amorphie.contract.core.Model.Dys;
using amorphie.contract.core.Response;
using amorphie.contract.core.Services;
using amorphie.contract.core.Services.Kafka;
using amorphie.contract.infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;


namespace amorphie.contract.application
{

    public class DocumentAppService : IDocumentAppService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IMinioService _minioService;
        private readonly IDysProducer _dysProducer;
        private readonly ITSIZLProducer _tsizlProducer;
        private readonly ITemplateEngineAppService _templateEngineAppService;
        private readonly ICustomerAppService _customerAppService;
        private readonly FileConverterFactory _fileConverterFactory;
        private readonly IUserSignedContractAppService _userSignedContractAppService;

        public DocumentAppService(ProjectDbContext projectDbContext, IMinioService minioService, IDysProducer dysProducer, ITSIZLProducer tsizlProducer, ITemplateEngineAppService templateEngineAppService, ICustomerAppService customerAppService, FileConverterFactory fileConverterFactory, IUserSignedContractAppService userSignedContractAppService)
        {
            _dbContext = projectDbContext;
            _minioService = minioService;
            _dysProducer = dysProducer;
            _tsizlProducer = tsizlProducer;
            _templateEngineAppService = templateEngineAppService;
            _customerAppService = customerAppService;
            _fileConverterFactory = fileConverterFactory;
            _userSignedContractAppService = userSignedContractAppService;
        }

        public async Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken)
        {

            var response = new List<RootDocumentDto>();

            var query = _dbContext!.Document
                                    .Skip(input.Page * input.PageSize)
                                    .Take(input.PageSize);


            if (!string.IsNullOrEmpty(input.Keyword))
            {
                query = query.Where(x => x.Status.ToString() == input.Keyword);
            }

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {

                response = mapToRootDocumentDto(securityQuestions);
            }

            return GenericResult<List<RootDocumentDto>>.Success(response);
        }

        public async Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentAll(CancellationToken cancellationToken)
        {
            var query = _dbContext!.Document;
            var response = new List<RootDocumentDto>();

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {
                response = mapToRootDocumentDto(securityQuestions);
            }

            return GenericResult<List<RootDocumentDto>>.Success(response);
        }

        public async Task<GenericResult<bool>> AddAsync(DocumentDto documentDto, string minioObjectName)
        {

            var document = new Document
            {
                Id = documentDto.Id,
                DocumentDefinitionId = documentDto.DocumentDefinitionId,
                Status = EStatus.Completed,
                CustomerId = documentDto.CustomerId,
                DocumentContent = ObjectMapperApp.Mapper.Map<DocumentContent>(documentDto.DocumentContent),
                DocumentInstanceNotes = ObjectMapperApp.Mapper.Map<List<DocumentInstanceNote>>(documentDto.Notes),
            };

            document.DocumentContent.MinioObjectName = minioObjectName;

            await _dbContext.Document.AddAsync(document);
            await _dbContext.SaveChangesAsync();

            return GenericResult<bool>.Success(true);
        }

        public async Task<GenericResult<bool>> Instance(DocumentInstanceInputDto input)
        {
            var docdef = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == input.DocumentCode && x.Semver == input.DocumentVersion);

            if (docdef is null)
                return GenericResult<bool>.Fail($"Document Code ve versiyona ait kayit bulunamadi! {input.DocumentCode}, {input.DocumentVersion}");

            // Metadata tag implementation -> IsTagImplemented

            if (docdef.DefinitionMetadata?.Any() == true) // && docdef?.DocumentDys is not null DYS olmayacak
            {
                CheckRequiredMetadata(docdef.DefinitionMetadata, input.InstanceMetadata);
            }

            var customerId = await _customerAppService.GetIdByReference(input.Reference);
            if (!customerId.IsSuccess)
            {
                var customerInputDto = new CustomerInputDto(input.Owner, input.Reference, input.CustomerNo);
                customerId = await _customerAppService.AddAsync(customerInputDto);
            }

            var documentDto = new DocumentDto
            {
                DocumentContent = input.DocumentContent,
                CustomerId = customerId.Data,
                DocumentDefinitionId = docdef.Id,
                Id = input.Id,
                Status = EStatus.Completed,
                Metadata = input.InstanceMetadata,
                Notes = input.Notes,
            };

            var response = await AddAsync(documentDto, input.ToString());
            if (!response.IsSuccess)
            {
                return response;
            }

            var converter = _fileConverterFactory.GetConverter(input.ContextType);
            byte[] fileByteArray = await converter.GetFileContentAsync(input.DocumentContent.FileContext);

            await _minioService.UploadFile(fileByteArray, input.ToString(), input.DocumentContent.ContentType, "");

            await _userSignedContractAppService.UpsertAsync(new UserSignedContractInputDto
            {
                ContractCode = "",
                ContractInstanceId =  "",
                DocumentInstanceIds = new List<Guid>()
            });

            if (docdef.DocumentDys is not null)
            {
                await SendToDys(docdef, input, fileByteArray);
            }

            if (docdef.DocumentTsizl is not null)
            {
                var request = new DoAutomaticEngagementPlainRequestDto(Convert.ToInt32(input.CustomerNo), docdef.DocumentTsizl.EngagementKind, StaticValuesExtensions.Fora.UserCode);
                await _tsizlProducer.PublishTSIZLData(request);
            }

            return GenericResult<bool>.Success(true);
        }

        private async Task SendToDys(DocumentDefinition docDef, DocumentInstanceInputDto inputDto, byte[] fileByteArray)
        {
            var documentDys = new DocumentDysRequestModel(inputDto.Owner, inputDto.DocumentCode, docDef.DocumentDys.ReferenceId.ToString(), docDef.Code, inputDto.ToString(), inputDto.DocumentContent.ContentType, fileByteArray);
            if (inputDto.InstanceMetadata is not null)
            {
                foreach (var item in inputDto.InstanceMetadata)
                {
                    documentDys.DocumentParameters.Add(item.Code, item.Data);
                }
            }
            await _dysProducer.PublishDysData(documentDys);
        }

        private GenericResult<bool> CheckRequiredMetadata(List<Metadata> docDefMetadata, List<MetadataDto> instanceMetadataList)
        {
            foreach (var item in docDefMetadata)
            {
                if (item.IsRequired && string.IsNullOrEmpty(item.Data))
                {
                    var conflictingProperty = instanceMetadataList.FirstOrDefault(x => x.Code == item.Code);
                    if (conflictingProperty == null)
                    {
                        return GenericResult<bool>.Fail($"Girilmesi zorunlu metadatalar bulunmaktadır {item.Code}");
                    }
                }
            }

            return GenericResult<bool>.Success(true);
        }

        public async Task<GenericResult<List<RootDocumentDto>>> GetAllMethod(PagedInputDto pagedInputDto, CancellationToken cancellationToken)
        {
            var query = _dbContext!.Document;
            var response = new List<RootDocumentDto>();

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {
                response = mapToRootDocumentDto(securityQuestions);
            }

            return GenericResult<List<RootDocumentDto>>.Success(response);
        }

        private List<RootDocumentDto> mapToRootDocumentDto(List<Document> documents)
        {
            return documents.Select(x =>
                         new RootDocumentDto
                         {
                             Id = x.Id,
                             DocumentDefinitionId = x.DocumentDefinitionId.ToString(),
                             StatuCode = x.Status.ToString(),
                             CreatedAt = x.CreatedAt,
                             DocumentDefinition = new DocumentDefinitionDto
                             {
                                 Code = x.DocumentDefinition.Code,
                                 Titles = x.DocumentDefinition.Titles,
                                 DocumentOperations = new DocumentOperationsDto
                                 {
                                     DocumentManuelControl = x.DocumentDefinition.DocumentOperations!.DocumentManuelControl,
                                     DocumentOperationsTagsDetail = ObjectMapperApp.Mapper.Map<List<TagDto>>(x.DocumentDefinition.DocumentOperations.DocumentOperationsTagsDetail!.Select(t => t.Tags))
                                 }
                             },
                             DocumentContent = ObjectMapperApp.Mapper.Map<DocumentContentDto>(x.DocumentContent)
                         }).ToList();

        }

        public async Task<GenericResult<DocumentDownloadOutputDto>> DownloadDocument(DocumentDownloadInputDto inputDto, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(inputDto.ObjectId, out Guid contentId))
            {
                return GenericResult<DocumentDownloadOutputDto>.Fail("ObjectId is not in a valid Guid format");
            }

            var userReference = inputDto.GetUserReference();

            var customerDoc = await _dbContext.Document.AsNoTracking().FirstOrDefaultAsync(
               c => c.Customer != null && c.Customer.Reference == userReference && c.DocumentContentId == contentId);

            if (customerDoc is null)
                throw new FileNotFoundException($"{inputDto.ObjectId} file not found for {userReference}");

            var minioObject = await _minioService.DownloadFile(customerDoc.DocumentContent.MinioObjectName, cancellationToken);

            var res = new DocumentDownloadOutputDto
            {
                FileType = minioObject.ContentType,
                FileContent = minioObject.FileContent,
                FileName = minioObject.FileName
            };

            return GenericResult<DocumentDownloadOutputDto>.Success(res);
        }

    }

    public interface IDocumentAppService
    {
        Task<GenericResult<bool>> AddAsync(DocumentDto documentDto, string minioObjectName);
        public Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken);
        public Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentAll(CancellationToken cancellationToken);
        Task<GenericResult<bool>> Instance(DocumentInstanceInputDto input);
        Task<GenericResult<DocumentDownloadOutputDto>> DownloadDocument(DocumentDownloadInputDto inputDto, CancellationToken cancellationToken);
    }
}

