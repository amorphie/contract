using amorphie.contract.core.CustomException;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Model.Documents;
using amorphie.contract.core.Response;
using amorphie.contract.infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Model;
using amorphie.contract.application.Extensions;

namespace amorphie.contract.application
{
    public interface IDocumentDefinitionAppService
    {
        Task<GenericResult<IEnumerable<DocumentDefinitionDto>>> GetAllDocumentDefinition(GetAllDocumentDefinitionInputDto input, CancellationToken cancellationToken);
        Task<GenericResult<DocumentDefinition>> CreateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id);
        Task<GenericResult<DocumentDefinition>> UpdateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id);
    }

    public class DocumentDefinitionAppService : IDocumentDefinitionAppService
    {
        private readonly ProjectDbContext _dbContext;

        public DocumentDefinitionAppService(ProjectDbContext projectDbContext)
        {
            _dbContext = projectDbContext;
        }

        public async Task<GenericResult<IEnumerable<DocumentDefinitionDto>>> GetAllDocumentDefinition(GetAllDocumentDefinitionInputDto input, CancellationToken cancellationToken)
        {
            var list = await _dbContext.DocumentDefinition.OrderBy(x => x.Code).Skip(input.Page * input.PageSize)
                .Take(input.PageSize).AsNoTracking().ToListAsync(cancellationToken);

            var responseDtos = list.Select(x => ObjectMapperApp.Mapper.Map<DocumentDefinitionDto>(x, opt => opt.Items[Lang.LangCode] = input.LangCode)).ToList();

            return GenericResult<IEnumerable<DocumentDefinitionDto>>.Success(responseDtos);
        }

        public async Task<GenericResult<DocumentDefinition>> CreateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id)
        {
            var isDefinitionCodeExist = _dbContext.DocumentDefinition.Any(x => x.Code == inputDto.Code); //Update methodu olduğu için aynı kodun farklı versiyonları kontrolü yapmıyorum. Kullanıcının update'e gitmesi gerek.

            if (isDefinitionCodeExist)
            {
                return GenericResult<DocumentDefinition>.Fail("Ayni Dokuman tanımı daha önce yapılmış");
            }

            var documentDefinition = new DocumentDefinition
            {
                Id = id,
                Code = inputDto.Code,
                Semver = inputDto.Version,
                Titles = inputDto.Titles,
                DefinitionMetadata = inputDto.Metadatas
            };

            if (inputDto.SizeOptimize)
            {
                documentDefinition.DocumentOptimizeId = FindDocumentOptimize(inputDto.TransformTo, id);
            }

            documentDefinition.DocumentOperationId = CreateAndGetIdDocumentOperation(inputDto.UploadTags, inputDto.DocumentManuelControl);

            if (inputDto.DocumentType.IndexOf("OnlineSign") > -1)
            {
                documentDefinition.DocumentOnlineSignId = CreateAndGetIdDocumentOnlineSign(inputDto.Templates, inputDto.RenderAllowedClients, id);

            }
            else if (inputDto.DocumentType.IndexOf("renderUpload") > -1)
            {
                documentDefinition.DocumentOnlineSignId = CreateAndGetIdDocumentOnlineSign(inputDto.Templates, inputDto.RenderAllowedClients, id);
                documentDefinition.DocumentUploadId = CreateAndGetIdDocumentUpload(inputDto.ScaRequired, inputDto.AllowedUploadFormats, inputDto.UploadAllowedClients, id);
            }
            else
            {
                documentDefinition.DocumentUploadId = CreateAndGetIdDocumentUpload(inputDto.ScaRequired, inputDto.AllowedUploadFormats, inputDto.UploadAllowedClients, id);
            }

            _dbContext.DocumentDefinition.Add(documentDefinition);

            CreateDocumentTag(inputDto.Tags, id);


            if (inputDto.IntegrationRecord.IsDysRecord)
            {
                CreateDocumentDys(inputDto.Metadatas.Where(x => !String.IsNullOrEmpty(x.Code)).ToList(), inputDto.IntegrationRecord, id);
            }

            if (inputDto.IntegrationRecord.IsTsizlRecord)
            {
                CreateDocumentTsizl(inputDto.IntegrationRecord, id);
            }

            _dbContext.SaveChanges();

            return GenericResult<DocumentDefinition>.Success(documentDefinition);
        }

        public async Task<GenericResult<DocumentDefinition>> UpdateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id)
        {
            var versionList = _dbContext.DocumentDefinition
                                .Where(x => x.Code == inputDto.Code)
                                .Select(x => x.Semver)
                                .ToArray();

            if (versionList.Length == 0)
            {
                throw new EntityNotFoundException("Document Definition", inputDto.Code);
            }

            var highestVersion = Versioning.FindHighestVersion(versionList);

            if (Versioning.CompareVersion(inputDto.Version, highestVersion))
            {
                return GenericResult<DocumentDefinition>.Fail($"Versiyon {highestVersion} dan daha büyük olmalı");
            }

            var documentDefinition = new DocumentDefinition
            {
                Id = id,
                Code = inputDto.Code,
                Semver = inputDto.Version,
                Titles = inputDto.Titles,
                DefinitionMetadata = inputDto.Metadatas
            };

            if (inputDto.SizeOptimize)
            {
                documentDefinition.DocumentOptimizeId = FindDocumentOptimize(inputDto.TransformTo, id);
            }

            documentDefinition.DocumentOperationId = CreateAndGetIdDocumentOperation(inputDto.UploadTags, inputDto.DocumentManuelControl);

            if (inputDto.DocumentType.IndexOf("OnlineSign") > -1)
            {
                documentDefinition.DocumentOnlineSignId = CreateAndGetIdDocumentOnlineSign(inputDto.Templates, inputDto.RenderAllowedClients, id);

            }
            else if (inputDto.DocumentType.IndexOf("renderUpload") > -1)
            {
                documentDefinition.DocumentOnlineSignId = CreateAndGetIdDocumentOnlineSign(inputDto.Templates, inputDto.RenderAllowedClients, id);
                documentDefinition.DocumentUploadId = CreateAndGetIdDocumentUpload(inputDto.ScaRequired, inputDto.AllowedUploadFormats, inputDto.UploadAllowedClients, id);
            }
            else
            {
                documentDefinition.DocumentUploadId = CreateAndGetIdDocumentUpload(inputDto.ScaRequired, inputDto.AllowedUploadFormats, inputDto.UploadAllowedClients, id);
            }

            _dbContext.DocumentDefinition.Add(documentDefinition);

            CreateDocumentTag(inputDto.Tags, id);


            if (inputDto.IntegrationRecord.IsDysRecord)
            {
                CreateDocumentDys(inputDto.Metadatas.Where(x => !String.IsNullOrEmpty(x.Code)).ToList(), inputDto.IntegrationRecord, id);
            }

            if (inputDto.IntegrationRecord.IsTsizlRecord)
            {
                CreateDocumentTsizl(inputDto.IntegrationRecord, id);
            }

            _dbContext.SaveChanges();

            return GenericResult<DocumentDefinition>.Success(documentDefinition);
        }

        private Guid FindDocumentOptimize(Guid transformTo, Guid documentId) //DocumentOptimize elden geçirilmeli
        {
            var documentOptimizeId = _dbContext.DocumentOptimize.Where(x => x.Size == true && x.DocumentOptimizeTypeId == transformTo).Select(x => x.Id).FirstOrDefault();

            if (documentOptimizeId == Guid.Empty)
            {
                throw new EntityNotFoundException("Document Optimize Type", transformTo.ToString());
            }

            return documentOptimizeId;
        }

        private void CreateDocumentTag(List<Guid> list, Guid documentId)
        {
            List<DocumentTagsDetail> entityList = new List<DocumentTagsDetail>();
            foreach (var tagId in list)
            {
                entityList.Add(new DocumentTagsDetail
                {
                    DocumentDefinitionId = documentId,
                    TagId = tagId
                });
            }

            _dbContext.DocumentTagsDetail.AddRange(entityList);
        }

        private Guid CreateAndGetIdDocumentOperation(List<Guid> list, bool manuelControl)
        {
            var entityList = list.Select(x => new DocumentOperationsTagsDetail
            {
                TagId = x,
            }).ToList();

            Guid id = Guid.NewGuid();

            var documentOperations = new DocumentOperations
            {
                Id = id,
                DocumentManuelControl = manuelControl,
                DocumentOperationsTagsDetail = entityList
            };

            _dbContext.DocumentOperations.Add(documentOperations);

            return id;
        }

        #region SetDocumentUpload
        private Guid CreateAndGetIdDocumentUpload(bool scaRequired, List<AllowedUploadFormatInputDto> allowedUploadFormats, List<Guid> uploadAllowedClients, Guid documentId)
        {
            var documentUploadId = Guid.NewGuid();

            var documentUpload = new DocumentUpload
            {
                Id = documentUploadId,
                Required = scaRequired
            };

            documentUpload.DocumentAllowedClientDetails = SetDocumentUploadAllowedClientDetail(uploadAllowedClients, documentId);
            documentUpload.DocumentFormatDetails = SetDocumentUploadFormatDetail(allowedUploadFormats, documentId);

            _dbContext.DocumentUpload.Add(documentUpload);

            return documentUploadId;
        }

        private List<DocumentFormatDetail> SetDocumentUploadFormatDetail(List<AllowedUploadFormatInputDto> allowedUploadFormats, Guid documentId)
        {
            var documentFormatDetail = allowedUploadFormats.Select(x => new DocumentFormatDetail
            {
                DocumentDefinitionId = documentId,
                DocumentFormat = new DocumentFormat
                {
                    DocumentFormatTypeId = x.Format,
                    DocumentSizeId = x.MaxSizeKilobytes
                }
            }).ToList();

            return documentFormatDetail;
        }

        private List<DocumentAllowedClientDetail> SetDocumentUploadAllowedClientDetail(List<Guid> uploadAllowedClients, Guid documentId)
        {
            var allowedClientDetail = uploadAllowedClients.Select(x => new DocumentAllowedClientDetail
            {
                DocumentDefinitionId = documentId,
                DocumentAllowedClientId = x
            }).ToList();

            return allowedClientDetail;
        }
        #endregion

        #region  SetDocumentOnlineSign
        private Guid CreateAndGetIdDocumentOnlineSign(List<TemplateInputDto> templates, List<Guid> allowedClients, Guid documentId)
        {
            var onlineSignId = Guid.NewGuid();

            var onlineSign = new DocumentOnlineSign
            {
                Id = onlineSignId,
            };

            onlineSign.Templates = SetDocumentOnlineSignTemplateDetails(templates, documentId);
            onlineSign.DocumentAllowedClientDetails = SetDocumentOnlineSignAllowedClientDetails(allowedClients, documentId);

            _dbContext.DocumentOnlineSign.Add(onlineSign);

            return onlineSignId;
        }

        private List<Template> SetDocumentOnlineSignTemplateDetails(List<TemplateInputDto> templates, Guid documentId)
        {
            var langTypes = _dbContext.LanguageType.ToList();
            var result = templates.Select(x => new Template
            {
                LanguageCode = langTypes.FirstOrDefault(a => a.Id == x.Language)?.Code,
                Code = x.Name,
                Version = x.Version
            }).ToList();

            return result;
        }

        private List<DocumentAllowedClientDetail> SetDocumentOnlineSignAllowedClientDetails(List<Guid> allowedClients, Guid documentId)
        {
            var allowedClientDetail = allowedClients.Select(x => new DocumentAllowedClientDetail
            {
                DocumentDefinitionId = documentId,
                DocumentAllowedClientId = x
            }).ToList();

            return allowedClientDetail;
        }
        #endregion

        #region Document_Dys
        private void CreateDocumentDys(List<Metadata> dysMetadata, IntegrationRecordInputDto integrationRecord, Guid documentId)
        {

            string stringElementName = string.Join(",", dysMetadata.ConvertAll(x => x.Title));
            string stringElementId = string.Join(",", dysMetadata.ConvertAll(x => x.Code));
            var documentDys = new DocumentDys
            {
                DocumentDefinitionId = documentId,
                ReferenceId = integrationRecord.ReferenceId,
                ReferenceName = integrationRecord.ReferenceName,
                Fields = stringElementId,
                TitleFields = stringElementName
            };
            if (integrationRecord.ReferenceKey != 0)
            {
                documentDys.ReferenceKey = integrationRecord.ReferenceKey;
            }

            _dbContext.DocumentDys.Add(documentDys);
        }

        private void CreateDocumentTsizl(IntegrationRecordInputDto integrationRecord, Guid documentId)
        {
            var documentTsizl = new DocumentTsizl
            {
                DocumentDefinitionId = documentId,
                EngagementKind = integrationRecord.EngangmentKind
            };

            _dbContext.DocumentTsizls.Add(documentTsizl);
        }
        #endregion
    }
}