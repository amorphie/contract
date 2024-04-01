using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.zeebe.Model.DocumentDefinitionDataModel;
using Newtonsoft.Json;
using amorphie.contract.core.Enum;
using amorphie.contract.zeebe.Helper;

namespace amorphie.contract.zeebe.Services
{
    public interface IDocumentDefinitionService
    {
        Task<DocumentDefinition> DataModelToDocumentDefinition(dynamic documentDefinitionDataDynamic, Guid id);
        Task<DocumentDefinition> DataModelToDocumentDefinitionUpdate(dynamic documentDefinitionDataDynamic, Guid id);
    }
    public class DocumentDefinitionService : IDocumentDefinitionService
    {
        ProjectDbContext _dbContext;
        DocumentDefinitionDataModel _documentDefinitionDataModel;
        DocumentDefinition _documentdef;
        dynamic? _documentDefinitionDataDynamic;
        public DocumentDefinitionService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private void SetDocumentDefinitionLanguageDetail()
        {

            var langTypes = _dbContext.LanguageType.ToDictionary(i => i.Id, i => i.Code);
            _documentdef.Titles = _documentDefinitionDataModel.data.Titles.ToDictionary(item => langTypes[ZeebeMessageHelper.StringToGuid(item.language)], item => item.title);
        }
        private void SetDocumentTagsDetails()
        {
            var list = _documentDefinitionDataModel.data.Tags.Select(
                           x => new DocumentTagsDetail
                           {
                               DocumentDefinitionId = _documentdef.Id,
                               TagId = ZeebeMessageHelper.StringToGuid(x)
                           }
                       ).ToList();
            _documentdef.DocumentTagsDetails = list;
        }
        private void SetDocumentOptimize()
        {
            if (_documentDefinitionDataModel.data.TransformTo != null)
            {
                var documentOptimizeType = _dbContext.DocumentOptimizeType.Where(x => x.Id == ZeebeMessageHelper.StringToGuid(_documentDefinitionDataModel.data.TransformTo)).FirstOrDefault();
                var documentOptimize = _dbContext.DocumentOptimize.Where(x => x.Size == _documentDefinitionDataModel.data.Size && x.DocumentOptimizeTypeId == ZeebeMessageHelper.StringToGuid(_documentDefinitionDataModel.data.TransformTo)).FirstOrDefault();

                if (documentOptimize != null)
                {
                    _documentdef.DocumentOptimizeId = documentOptimize.Id;
                }
                else if (documentOptimizeType != null)
                {
                    _documentdef.DocumentOptimize = new DocumentOptimize
                    {
                        DocumentOptimizeTypeId = documentOptimizeType.Id,
                        Size = _documentDefinitionDataModel.data.Size
                    };
                }
            }
        }
        private void SetDocumentOperation()
        {
            if (_documentDefinitionDataModel.data.UploadTags == null)
                return;
            var manuelControl = _documentDefinitionDataModel.data.DocumentManuelControl;
            var list2 = _documentDefinitionDataModel.data.UploadTags.Select(x => new DocumentOperationsTagsDetail
            {
                TagId = ZeebeMessageHelper.StringToGuid(x),
            }).ToList();
            _documentdef.DocumentOperations =
                        new DocumentOperations
                        {
                            // DocumentDefinitionId = _documentdef.Id,
                            DocumentManuelControl = manuelControl,
                            DocumentOperationsTagsDetail = list2
                        };


        }
        private List<MetadataElement> SetDocumentEOV()
        {
            var dysTagField = new List<MetadataElement>();
            foreach (var entityPropertyData in _documentDefinitionDataModel.data.EntityProperty)
            {
                if (_documentDefinitionDataModel.data.disabledDataMetadata != null && _documentDefinitionDataModel.data.disabledDataMetadata.Any())
                {

                    var matchedMetadataElement = _documentDefinitionDataModel.data.disabledDataMetadata.FirstOrDefault(metadataElement =>
                        metadataElement.ElementName == entityPropertyData.PropertyName);

                    if (matchedMetadataElement != null)
                    {
                        entityPropertyData.PropertyName = matchedMetadataElement.ElementID;
                        dysTagField.Add(matchedMetadataElement);
                    }
                }
                var entityProperty = new amorphie.contract.core.Entity.EAV.EntityProperty
                {
                    EEntityPropertyType = (ushort)EEntityPropertyType.str,
                    EntityPropertyValue = new core.Entity.EAV.EntityPropertyValue { Data = entityPropertyData.value },
                    Code = entityPropertyData.PropertyName,
                    Required = entityPropertyData.required
                };

                var documentEntityProperty = new DocumentEntityProperty
                {
                    DocumentDefinitionId = _documentdef.Id,
                    EntityProperty = entityProperty
                };

                _documentdef.DocumentEntityPropertys.Add(documentEntityProperty);
            }
            return dysTagField;
        }


        #region SetDocumentUpload
        private void SetDocumentUploadAllowedClientDetail()
        {
            var allowedClientDetail = _documentDefinitionDataModel.data.UploadAllowedClients.Select(x => new DocumentAllowedClientDetail
            {
                DocumentDefinitionId = _documentdef.Id,
                DocumentAllowedClientId = ZeebeMessageHelper.StringToGuid(x.id)

            }).ToList();

            _documentdef.DocumentUpload.DocumentAllowedClientDetails = allowedClientDetail;

        }
        private void SetDocumentUploadFormatDetail()
        {
            var documentFormatDetail = _documentDefinitionDataModel.data.AllowedFormatsUploadList.Select(x => new DocumentFormatDetail
            {
                DocumentDefinitionId = _documentdef.Id,
                DocumentFormat = new DocumentFormat
                {
                    DocumentFormatTypeId = ZeebeMessageHelper.StringToGuid(x.Format),
                    DocumentSizeId = ZeebeMessageHelper.StringToGuid(x.MaxSizeKilobytes)
                }
            }).ToList();

            _documentdef.DocumentUpload.DocumentFormatDetails = documentFormatDetail;
        }

        private void SetDocumentUpload()
        {
            if (_documentdef.DocumentUpload == null)
            {
                _documentdef.DocumentUpload = new DocumentUpload();
            }
            _documentdef.DocumentUpload.Required = _documentDefinitionDataModel.data.ScaRequired;
            SetDocumentUploadAllowedClientDetail();
            SetDocumentUploadFormatDetail();

        }
        #endregion

        #region  SetDocumentOnlineSing
        private void SetDocumentOnlineSing()
        {
            if (_documentdef.DocumentOnlineSing == null)
            {
                _documentdef.DocumentOnlineSing = new DocumentOnlineSing();

            }

            SetDocumentOnlineSingTemplateDetails();
            SetDocumentOnlineSingAllowedClientDetails();

        }
        private void SetDocumentOnlineSingTemplateDetails()
        {
            var documentTemplateDetail = _documentDefinitionDataModel.data.TemplateList.Select(x => new DocumentTemplateDetail
            {
                DocumentDefinitionId = _documentdef.Id,
                DocumentTemplate = new DocumentTemplate
                {
                    LanguageTypeId = ZeebeMessageHelper.StringToGuid(x.language),
                    Code = x.RenderTemplate.name,
                    Version = x.version
                }
            }).ToList();
            _documentdef.DocumentOnlineSing.DocumentTemplateDetails = documentTemplateDetail;
        }
        private void SetDocumentOnlineSingAllowedClientDetails()
        {
            var allowedClientDetail = _documentDefinitionDataModel.data.RenderAllowedClients.Select(x => new DocumentAllowedClientDetail
            {
                DocumentDefinitionId = _documentdef.Id,
                DocumentAllowedClientId = ZeebeMessageHelper.StringToGuid(x)

            }).ToList();

            _documentdef.DocumentOnlineSing.DocumentAllowedClientDetails = allowedClientDetail;
        }
        #endregion

        #region  SetDocumentRender
        ///  olmicak
        // private void SetDocumentRender()
        // {
        //     if (_documentdef.DocumentRender == null)
        //     {
        //         _documentdef.DocumentRender = new DocumentRender();
        //     }
        //     SetDocumentRenderTemplateDetails();
        //     SetDocumentRenderAllowedClientDetails();

        // }
        // private void SetDocumentRenderTemplateDetails()
        // {

        // }
        // private void SetDocumentRenderAllowedClientDetails()
        // {

        // }
        #endregion

        #region Document_Dys

        private void SetDocumentDys(List<MetadataElement> dysMetadata)
        {
            if (_documentDefinitionDataModel.data.referenceId != 0 && !string.IsNullOrEmpty(_documentDefinitionDataModel.data.referenceName))
            {
                try
                {
                    string stringElementName = string.Join(",", dysMetadata.ConvertAll(element => element.ElementName));
                    string stringElementId = string.Join(",", dysMetadata.ConvertAll(element => element.ElementID));
                    var documentDys = new DocumentDys
                    {
                        ReferenceId = _documentDefinitionDataModel.data.referenceId,
                        ReferenceName = _documentDefinitionDataModel.data.referenceName,
                        Fields = stringElementId,
                        TitleFields = stringElementName
                    };
                    if (_documentDefinitionDataModel.data.referenceKey != 0)
                    {
                        documentDys.ReferenceKey = _documentDefinitionDataModel.data.referenceKey;
                    }
                    _documentdef.DocumentDys = documentDys;
                }
                catch (Exception e)
                {

                    throw new ArgumentException(e.Message);
                }
            }
        }

        private void SetDocumentTsizl()
        {
            if (!string.IsNullOrEmpty(_documentDefinitionDataModel.data.engangmentKind))
            {
                var documentTsizl = new DocumentTsizl
                {
                    EngagementKind = _documentDefinitionDataModel.data.engangmentKind
                };
                _documentdef.DocumentTsizl = documentTsizl;
            }
        }

        #endregion

        private void DynamicToDocumentDefinitionDataModel()
        {
            _documentDefinitionDataModel = new DocumentDefinitionDataModel();
            _documentDefinitionDataModel.data = new Data();

            _documentDefinitionDataModel.data = JsonConvert.DeserializeObject<Data>(_documentDefinitionDataDynamic);
        }


        public async Task<DocumentDefinition> DataModelToDocumentDefinition(dynamic documentDefinitionDataDynamic, Guid id)
        {
            _documentDefinitionDataDynamic = documentDefinitionDataDynamic;
            try
            {
                DynamicToDocumentDefinitionDataModel();
                var documentDefinition = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == _documentDefinitionDataModel.data.Code && x.Semver == _documentDefinitionDataModel.data.versiyon);
                if (documentDefinition != null)
                {
                    throw new Exception("Ayni Dokuman tanımı daha önce yapılmıs");
                }
                else
                {
                    _documentdef = new DocumentDefinition
                    {
                        Id = id,
                        Code = _documentDefinitionDataModel.data.Code,
                        Status = EStatus.OnHold,
                        BaseStatus = EStatus.OnHold,
                        Semver = _documentDefinitionDataModel.data.versiyon

                    };
                }
                SetDocumentDefinitionLanguageDetail();
                SetDocumentTagsDetails();
                SetDocumentOptimize();
                SetDocumentOperation();
                var dysMetadata = SetDocumentEOV();
                SetDocumentDys(dysMetadata);
                SetDocumentTsizl();

                if (_documentDefinitionDataModel.data.DocumentType.IndexOf("onlineSing") > -1)
                {
                    SetDocumentOnlineSing();

                }
                else if (_documentDefinitionDataModel.data.DocumentType.IndexOf("renderUpload") > -1)
                {
                    SetDocumentOnlineSing();
                    SetDocumentUpload();
                }
                else
                {
                    SetDocumentUpload();
                }

                _documentdef.Status = EStatus.Active;
                _documentdef.BaseStatus = EStatus.Active;

                _dbContext.DocumentDefinition.Add(_documentdef);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _documentdef;

        }

        public async Task<DocumentDefinition> DataModelToDocumentDefinitionUpdate(dynamic documentDefinitionDataDynamic, Guid id)
        {
            _documentDefinitionDataDynamic = documentDefinitionDataDynamic;
            try
            {
                DynamicToDocumentDefinitionDataModel();
                var versionList = _dbContext.DocumentDefinition
                                    .Select(x => x.Semver)
                                    .ToArray();

                var highestVersion = StringHelper.GetHighestVersion(versionList);

                if (StringHelper.CompareVersions(_documentDefinitionDataModel.data.versiyon, highestVersion) <= 0)
                {
                    throw new ArgumentException($"Versiyon {highestVersion} dan daha büyük olmalı");
                }
                var documentDefinition = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == _documentDefinitionDataModel.data.Code && x.Semver == _documentDefinitionDataModel.data.versiyon);
                if (documentDefinition != null)
                {
                    throw new ArgumentException("Ayni Dokuman tanımı daha önce yapılmıs");
                }
                else
                {
                    _documentdef = new DocumentDefinition
                    {
                        Id = id,
                        Code = _documentDefinitionDataModel.data.Code,
                        Status = EStatus.OnHold,
                        BaseStatus = EStatus.OnHold,
                        Semver = _documentDefinitionDataModel.data.versiyon

                    };
                }

                SetDocumentDefinitionLanguageDetail();
                SetDocumentTagsDetails();
                SetDocumentOptimize();
                SetDocumentOperation();
                SetDocumentEOV();

                if (_documentDefinitionDataModel.data.DocumentType.IndexOf("onlineSing") > -1)
                {
                    SetDocumentOnlineSing();

                }
                else if (_documentDefinitionDataModel.data.DocumentType.IndexOf("renderUpload") > -1)
                {
                    SetDocumentOnlineSing();
                    SetDocumentUpload();
                }
                else
                {
                    SetDocumentUpload();
                }

                _documentdef.Status = EStatus.Active;
                _documentdef.BaseStatus = EStatus.Active;

                _dbContext.DocumentDefinition.Add(_documentdef);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _documentdef;
        }
    }
}