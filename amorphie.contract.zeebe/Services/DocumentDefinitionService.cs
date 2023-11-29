using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Model.DocumentDefinitionDataModel;
using amorphie.contract.zeebe.Services.Interfaces;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Enum;

namespace amorphie.contract.zeebe.Services
{
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
            var multiLanguageList = _documentDefinitionDataModel.data.Titles.Select(x => new MultiLanguage
            {
                Name = x.title,
                LanguageTypeId = ZeebeMessageHelper.StringToGuid(x.language),
                Code = _documentdef.Code
            }).ToList();

            _documentdef.DocumentDefinitionLanguageDetails = multiLanguageList.Select(x => new DocumentDefinitionLanguageDetail
            {
                DocumentDefinitionId = _documentdef.Id,
                MultiLanguage = x
            }).ToList();
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
                _documentdef.DocumentOptimize = new DocumentOptimize
                {
                    DocumentOptimizeType = new DocumentOptimizeType
                    {
                        Id = ZeebeMessageHelper.StringToGuid(_documentDefinitionDataModel.data.TransformTo)
                    },
                    Size = _documentDefinitionDataModel.data.Size,
                    DocumentDefinitionId = _documentdef.Id
                };
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
                            DocumentDefinitionId = _documentdef.Id,
                            DocumentManuelControl = manuelControl,
                            DocumentOperationsTagsDetail = list2
                        };


        }
        private void SetDocumentEOV()
        {
            var ep = _documentDefinitionDataModel.data.EntityProperty.Select(x => new amorphie.contract.core.Entity.EAV.EntityProperty
            {
                EEntityPropertyType =  (ushort)EEntityPropertyType.str,
                EntityPropertyValue = new core.Entity.EAV.EntityPropertyValue { Data = x.value },
                Code = x.PropertyName
            }).ToList();
            var epdb = new amorphie.contract.core.Entity.EAV.EntityProperty();

            foreach (var i in ep)
            {
                var epdbf = _dbContext.EntityProperty.FirstOrDefault(x => x.Code == i.Code);
                if (epdbf != null)
                {
                    epdb = epdbf;
                }
                epdb.Code = i.Code;

                var epv = _dbContext.EntityPropertyValue.FirstOrDefault(x => x.Data == i.EntityPropertyValue.Data);
                if (epv != null)
                {
                    epdb.EntityPropertyValue = epv;
                }
                else
                {
                    epdb.EntityPropertyValue = i.EntityPropertyValue;
                }
                epdb.EEntityPropertyType  =  (ushort)EEntityPropertyType.str;
                var dep = _dbContext.DocumentEntityProperty.FirstOrDefault(x => x.EntityProperty.Code == i.Code);
                if (dep == null)
                {
                    _documentdef.DocumentEntityPropertys.Add(new DocumentEntityProperty
                    {
                        DocumentDefinitionId = _documentdef.Id,
                        EntityProperty = epdb,
                    });
                }
            }
        }


        #region SetDocumentUpload
        private void SetDocumentUploadAllowedClientDetail()
        {
            var allowedClientDetail = _documentDefinitionDataModel.data.UploadAllowedClients.Select(x => new DocumentAllowedClientDetail
            {
                DocumentDefinitionId = _documentdef.Id,
                DocumentAllowedClientId = ZeebeMessageHelper.StringToGuid(x)

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
                    DocumentFormatTypeId = ZeebeMessageHelper.StringToGuid(x.format),
                    DocumentSizeId = ZeebeMessageHelper.StringToGuid(x.maxsizekilobytes)
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
            _documentdef.DocumentOnlineSing.Semver = _documentDefinitionDataModel.data.versiyon;

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
                    Code = x.RenderTemplate
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
                var onHoldStatus = _dbContext.Status.FirstOrDefault(x => x.Code == "on-hold");
                if (onHoldStatus == null)
                {
                    onHoldStatus = new Status { Code = "on-hold" };
                }
                var documentDefinition = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == _documentDefinitionDataModel.data.Code);
                if (documentDefinition != null)
                {
                    _documentdef = documentDefinition;
                }
                else
                {
                    _documentdef = new DocumentDefinition
                    {
                        Id = id,
                        Code = _documentDefinitionDataModel.data.Code,
                        Status = onHoldStatus,
                        BaseStatus = onHoldStatus
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