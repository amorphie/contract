using System.Data;
using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.application.Customer;
using amorphie.contract.application.Customer.Dto;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.core;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Model;
using amorphie.contract.core.Model.Colleteral;
using amorphie.contract.core.Model.Dys;
using amorphie.contract.core.Model.Minio;
using amorphie.contract.core.Response;
using amorphie.contract.core.Services;
using amorphie.contract.core.Services.Kafka;
using amorphie.contract.infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Serilog;


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
        private readonly IUserSignedContractAppService _userSignedContractAppService;
        private readonly IContractAppService _contractAppService;
        private readonly IPdfManager _pdfManager;
        private readonly ILogger _logger;
        private readonly ITagAppService _tagAppService;

        public DocumentAppService(
            ProjectDbContext projectDbContext,
            IMinioService minioService,
            IDysProducer dysProducer,
            ITSIZLProducer tsizlProducer,
            ITemplateEngineAppService templateEngineAppService,
            ICustomerAppService customerAppService,
            IUserSignedContractAppService userSignedContractAppService,
            IContractAppService contractAppService,
            IPdfManager pdfManager,
            ILogger logger,
            ITagAppService tagAppService)
        {
            _dbContext = projectDbContext;
            _minioService = minioService;
            _dysProducer = dysProducer;
            _tsizlProducer = tsizlProducer;
            _templateEngineAppService = templateEngineAppService;
            _customerAppService = customerAppService;
            _userSignedContractAppService = userSignedContractAppService;
            _contractAppService = contractAppService;
            _pdfManager = pdfManager;
            _logger = logger;
            _tagAppService = tagAppService;
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

        public async Task<GenericResult<Guid>> AddAsync(DocumentDto documentDto, string minioObjectName)
        {

            var document = new Document
            {
                Id = documentDto.Id,
                DocumentDefinitionId = documentDto.DocumentDefinitionId,
                Status = documentDto.Status,
                CustomerId = documentDto.CustomerId,
                DocumentContent = ObjectMapperApp.Mapper.Map<DocumentContent>(documentDto.DocumentContent),
                DocumentInstanceNotes = ObjectMapperApp.Mapper.Map<List<DocumentInstanceNote>>(documentDto.Notes),
                InstanceMetadata = ObjectMapperApp.Mapper.Map<List<Metadata>>(documentDto.Metadata)
            };

            document.DocumentContent.MinioObjectName = minioObjectName;

            await _dbContext.Document.AddAsync(document);
            await _dbContext.SaveChangesAsync();

            return GenericResult<Guid>.Success(document.Id);
        }

        public async Task<GenericResult<bool>> ApproveInstance(ApproveDocumentInstanceInputDto input)
        {

            var customerId = await _customerAppService.GetIdByReference(input.HeaderModel.UserReference);
            if (!customerId.IsSuccess)
            {
                return GenericResult<bool>.Fail($"Müşteri bulunamadı. {input.HeaderModel.UserReference}");
            }

            var documentInstance = await _dbContext.Document
                        .FirstOrDefaultAsync(k => k.CustomerId == customerId.Data && k.Id == input.DocumentInstanceId);

            if (documentInstance is null)
            {
                _logger.Error("Document not found. {UserReference} - {DocumentInstanceId}", input.HeaderModel.UserReference, input.DocumentInstanceId);

                return GenericResult<bool>.Fail("Doküman bulunamadı.");
            }

            if (documentInstance.Status == ApprovalStatus.Approved)
            {
                return GenericResult<bool>.Success(true);
            }

            if (!String.IsNullOrWhiteSpace(input.ContractCode))
            {
                var contractExists = await CheckIfContractExistsAsync(input.ContractCode, input.HeaderModel.EBankEntity);

                if (!contractExists)
                {
                    return GenericResult<bool>.Fail($"Kontrat bulunamadı. {input.ContractCode}");
                }
            }

            await SaveUserSignedContract(input.ContractCode, input.ContractInstanceId, documentInstance.Id, ApprovalStatus.Approved, input.HeaderModel.UserReference);

            var documentContent = await _minioService.DownloadFile(documentInstance.DocumentContent.MinioObjectName, new CancellationToken());
            byte[] fileByteArray = Convert.FromBase64String(documentContent.FileContent);

            var documentDef = documentInstance?.DocumentDefinition;

            //Update document approval status
            UploadFileModel uploadFileModel = new()
            {
                Data = fileByteArray,
                FileName = documentInstance.Id.ToString(),
                ContentType = documentContent.ContentType,
                DocumentDefinitionCode = documentDef.Code,
                DocumentDefinitionVersion = documentDef.Semver,
                Reference = input.HeaderModel.UserReference,
                ApprovalStatus = ApprovalStatus.Approved,
                ContractDefinitionCode = input.ContractCode
            };

            await _minioService.UploadFile(uploadFileModel);

            documentInstance.SetApprovalStatusToApproved();

            await _dbContext.SaveChangesAsync();

            // TODO: flow içinde ayrı bir task servis olarak çalıştırılacak.
            if (documentDef?.DocumentDys is not null)
            {

                var sendDysModel = new DocumentDysRequestModel(input.HeaderModel.UserReference,
                                                documentDef.Code,
                                                documentDef.Semver,
                                                documentDef.DocumentDys.ReferenceId.ToString(),
                                                documentDef.Code, documentContent.FileName,
                                                documentContent.ContentType,
                                                fileByteArray);

                await SendToDys(sendDysModel, documentInstance.InstanceMetadata);
            }

            if (documentDef?.DocumentTsizl is not null)
            {
                var request = new DoAutomaticEngagementPlainRequestDto(Convert.ToInt32(input.HeaderModel.CustomerNo), documentDef.DocumentTsizl.EngagementKind, StaticValuesExtensions.Fora.UserCode);
                await _tsizlProducer.PublishTSIZLData(request);
            }

            return GenericResult<bool>.Success(true);

        }

        public async Task<GenericResult<DocumentInstanceOutputDto>> Instance(DocumentInstanceInputDto input)
        {
            if (!AppConsts.AllowedContentTypes.Contains(input.DocumentContent.ContentType))
            {
                return GenericResult<DocumentInstanceOutputDto>.Fail($"İzin verilmeyen doküman tipi gönderildi. {input.DocumentCode}, {input.DocumentVersion}, {input.DocumentContent.ContentType}");
            }

            if (_dbContext.Document.Any(x => x.Id == input.RenderId))
            {
                return GenericResult<DocumentInstanceOutputDto>.Fail("İlgili render edilmiş doküman zaten başka bir hesap için tanımlanmış.");
            }

            if (!String.IsNullOrWhiteSpace(input.ContractCode))
            {
                var contractExists = await CheckIfContractExistsAsync(input.ContractCode, input.HeaderModel.EBankEntity);

                if (!contractExists)
                {
                    return GenericResult<DocumentInstanceOutputDto>.Fail($"Kontrat bulunamadı. {input.ContractCode}");
                }
            }

            var originalContent = await _templateEngineAppService.GetRenderPdf(input.RenderId.ToString());

            if (!originalContent.IsSuccess)
            {
                return GenericResult<DocumentInstanceOutputDto>.Fail("Orjinal dokümanın render bilgisi bulunamadı.");
            }

            var isVerified = _pdfManager.VerifyPdfContent(originalContent.Data, input.DocumentContent.FileContext);

            if (!isVerified)
            {
                return GenericResult<DocumentInstanceOutputDto>.Fail($"İmzalanan doküman ile orjinal doküman aynı değil. {input.DocumentCode}, {input.DocumentVersion}");
            }

            var docdef = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == input.DocumentCode && x.Semver == input.DocumentVersion);

            if (docdef is null)
                return GenericResult<DocumentInstanceOutputDto>.Fail($"Document Code ve versiyona ait kayit bulunamadi! {input.DocumentCode}, {input.DocumentVersion}");

            // Metadata tag implementation -> IsTagImplemented

            var metadataDtoList = await TagMetadataAsync(docdef.DefinitionMetadata, input.InstanceMetadata, input.HeaderModel);

            if (docdef.DefinitionMetadata?.Any() == true) // && docdef?.DocumentDys is not null DYS olmayacak
            {
                CheckRequiredMetadata(docdef.DefinitionMetadata, metadataDtoList);
            }

            var customerId = await _customerAppService.GetIdByReference(input.HeaderModel.UserReference);
            if (!customerId.IsSuccess)
            {
                var customerInputDto = new CustomerInputDto(input.HeaderModel.UserReference, input.HeaderModel.UserReference, input.HeaderModel.CustomerNo, "");
                customerId = await _customerAppService.AddAsync(customerInputDto);
            }

            var documentDto = new DocumentDto
            {
                Id = input.RenderId,
                DocumentContent = input.DocumentContent,
                CustomerId = customerId.Data,
                DocumentDefinitionId = docdef.Id,
                Status = ApprovalStatus.TemporarilyApproved,
                Metadata = metadataDtoList,
                Notes = input.Notes,
            };

            byte[] fileByteArray = Convert.FromBase64String(input.DocumentContent.FileContext);

            UploadFileModel uploadFileModel = new()
            {
                Data = fileByteArray,
                FileName = documentDto.Id.ToString(),
                ContentType = input.DocumentContent.ContentType,
                DocumentDefinitionCode = docdef.Code,
                DocumentDefinitionVersion = docdef.Semver,
                Reference = input.HeaderModel.UserReference,
                ApprovalStatus = ApprovalStatus.TemporarilyApproved,
                ContractDefinitionCode = input.ContractCode
            };

            var documentInsertResponse = await AddAsync(documentDto, uploadFileModel.ObjectName);
            if (!documentInsertResponse.IsSuccess)
            {
                return GenericResult<DocumentInstanceOutputDto>.Fail(documentInsertResponse.ErrorMessage);
            }

            await _minioService.UploadFile(uploadFileModel);

            await SaveUserSignedContract(input.ContractCode, input.ContractInstanceId, documentInsertResponse.Data, ApprovalStatus.TemporarilyApproved, input.HeaderModel.UserReference);

            return GenericResult<DocumentInstanceOutputDto>.Success(new DocumentInstanceOutputDto
            {
                DocumentInstanceId = documentInsertResponse.Data
            });
        }


        #region Private Methods

        private async Task<bool> CheckIfContractExistsAsync(string contractCode, EBankEntity eBankEntity)
        {
            var checkExistContract = await _contractAppService.GetExist(new ContractGetExistInputDto
            {
                Code = contractCode,
                EBankEntity = eBankEntity

            }, CancellationToken.None);

            return checkExistContract.Data;
        }


        private async Task SendToDys(DocumentDysRequestModel documentDys, IEnumerable<Metadata> metaData)
        {
            if (metaData is not null)
            {
                foreach (var item in metaData)
                {
                    if (item.Code == "Field08TCKimlik")
                    {
                        documentDys.DocumentParameters["Field08TCKimlik"] = item.Data;
                    }
                    else if (item.Code == "Field09VergiNo")
                    {
                        documentDys.DocumentParameters["Field09VergiNo"] = item.Data;
                    }
                    else
                    {

                        documentDys.DocumentParameters.Add(item.Code, item.Data);
                    }
                }
            }
            await _dysProducer.PublishDysData(documentDys);
        }
        private async Task<List<MetadataDto>> TagMetadataAsync(List<Metadata> docdefMetadata, List<MetadataDto> instanceMetadata, HeaderFilterModel headerModel)
        {
            var metadataDtos = ObjectMapperApp.Mapper.Map<List<MetadataDto>>(docdefMetadata);
            var getTagMetadata = await _tagAppService.GetTagMetadata(metadataDtos, headerModel);

            List<MetadataDto> metadataDtoList = new();
            if (getTagMetadata.IsSuccess && getTagMetadata.Data is not null)
            {
                foreach (var item in getTagMetadata.Data)
                {

                    metadataDtoList.Add(new MetadataDto
                    {
                        Code = item.Key,
                        Data = item.Value
                    });
                }
            }
            if (instanceMetadata is not null)
            {
                foreach (var item in instanceMetadata)
                {
                    if (!metadataDtoList.Exists(x => x.Code == item.Code))
                    {
                        metadataDtoList.Add(item);
                    }
                }
            }
            return metadataDtoList;
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

        private async Task SaveUserSignedContract(string contractCode, Guid? contractInstanceId, Guid documentInstanceId, ApprovalStatus approvalStatus, string userReference)
        {

            if (!String.IsNullOrEmpty(contractCode) && contractInstanceId.HasValue && contractInstanceId != Guid.Empty)
            {

                var userSigned = new UserSignedContractInputDto
                {
                    ContractCode = contractCode,
                    ContractInstanceId = contractInstanceId.Value,
                    DocumentInstanceIds = [documentInstanceId],
                    ApprovalStatus = approvalStatus,
                };

                userSigned.SetHeaderParameters(userReference);

                var upsertResult = await _userSignedContractAppService.UpsertAsync(userSigned);


                if (!upsertResult.IsSuccess)
                {
                    _logger.Error("Failed to upsert userSignedContract. {Message} - {ContractCode} {ContractInstanceId}", upsertResult.ErrorMessage, contractCode, contractInstanceId);
                }
            }

        }

        #endregion

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


        public async Task<GenericResult<DocumentDownloadOutputDto>> DownloadDocument(DocumentDownloadInputDto inputDto, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(inputDto.ObjectId, out Guid contentId))
            {
                return GenericResult<DocumentDownloadOutputDto>.Fail("ObjectId is not in a valid Guid format");
            }

            var userReference = inputDto.GetUserReference();

            var customerDoc = await _dbContext.Document
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                            c => c.Customer.Reference == userReference && c.DocumentContentId == contentId);

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

        public async Task<GenericResult<List<DocumentInstanceDto>>> GetDocumentsToApprove(GetDocumentsToApproveInputDto input)
        {
            var documents = await _dbContext.DocumentDefinition
                                    .AsNoTracking()
                                    .Where(k => input.DocumentCodes.Select(k => k.Code).Any(x => x == k.Code)).ToListAsync();


            List<DocumentInstanceDto> documentInstanceDtos = new();

            foreach (var doc in documents)
            {
                var lastVersion = Versioning.FindHighestVersion(documents.Where(x => x.Code == doc.Code).Select(x => x.Semver).ToArray());

                if (doc.Semver == lastVersion)
                {
                    var template = doc?.DocumentOnlineSign?.Templates.FirstOrDefault(x => x.LanguageCode == input.HeaderModel.LangCode);

                    DocumentInstanceDto docInstance = new()
                    {
                        Code = doc.Code,
                        MinVersion = doc.Semver,
                        LastVersion = lastVersion,
                        Name = doc.Titles.L(input.HeaderModel.LangCode),
                        DocumentDetail = new DocumentInstanceDetailDto()
                        {
                            OnlineSign = new DocumentInstanceOnlineSignDto
                            {
                                TemplateCode = template.Code,
                                Version = template.Version
                            }
                        }
                    };

                    documentInstanceDtos.Add(docInstance);
                }
            }

            return GenericResult<List<DocumentInstanceDto>>.Success(documentInstanceDtos);
        }

        public async Task<GenericResult<bool>> MigrateDocument(MigrateDocumentInputDto input)
        {
            var documentDto = new DocumentDto
            {
                Id = Guid.NewGuid(),
                DocumentContent = input.DocumentContent,
                CustomerId = input.CustomerId,
                DocumentDefinitionId = input.DocumentDefinitionId,
                Status = ApprovalStatus.TemporarilyApproved,
                Metadata = input.InstanceMetadata,
                Notes = input.Notes,
            };

            byte[] fileByteArray = Convert.FromBase64String(input.DocumentContent.FileContext);

            string contractCodes = string.Join(",", input.DocumentMigrationContracts.Select(c => c.Code));

            UploadFileModel uploadFileModel = new()
            {
                Data = fileByteArray,
                FileName = documentDto.Id.ToString(),
                ContentType = input.DocumentContent.ContentType,
                DocumentDefinitionCode = input.DocumentCode,
                DocumentDefinitionVersion = input.DocumentVersion,
                Reference = input.UserReference,
                ApprovalStatus = ApprovalStatus.Approved,
                ContractDefinitionCode = contractCodes
            };

            var documentInsertResponse = await AddAsync(documentDto, uploadFileModel.ObjectName);
            if (!documentInsertResponse.IsSuccess)
            {
                return GenericResult<bool>.Fail(documentInsertResponse.ErrorMessage);
            }

            await _minioService.UploadFile(uploadFileModel);

            if (input.DocumentContentOriginal is not null)
            {
                UploadFileModel uploadFileModelOriginal = new()
                {
                    Data = Convert.FromBase64String(input.DocumentContentOriginal.FileContext),
                    FileName = documentDto.Id.ToString(),
                    ContentType = input.DocumentContentOriginal.ContentType,
                    DocumentDefinitionCode = input.DocumentCode,
                    DocumentDefinitionVersion = input.DocumentVersion,
                    Reference = input.UserReference,
                    ApprovalStatus = ApprovalStatus.Approved,
                    ContractDefinitionCode = contractCodes
                };

                 await _minioService.UploadFile(uploadFileModelOriginal);
            }

            foreach (var c in input.DocumentMigrationContracts)
            {
                await SaveUserSignedContract(c.Code, Guid.NewGuid(), documentInsertResponse.Data, ApprovalStatus.Approved, input.UserReference);
            }
            return GenericResult<bool>.Success(true);

        }
    }

    public interface IDocumentAppService
    {
        Task<GenericResult<Guid>> AddAsync(DocumentDto documentDto, string minioObjectName);
        public Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken);
        public Task<GenericResult<List<RootDocumentDto>>> GetAllDocumentAll(CancellationToken cancellationToken);
        Task<GenericResult<bool>> ApproveInstance(ApproveDocumentInstanceInputDto input);
        Task<GenericResult<DocumentInstanceOutputDto>> Instance(DocumentInstanceInputDto input);
        Task<GenericResult<DocumentDownloadOutputDto>> DownloadDocument(DocumentDownloadInputDto inputDto, CancellationToken cancellationToken);
        Task<GenericResult<List<DocumentInstanceDto>>> GetDocumentsToApprove(GetDocumentsToApproveInputDto input);
        Task<GenericResult<bool>> MigrateDocument(MigrateDocumentInputDto input);
    }
}

