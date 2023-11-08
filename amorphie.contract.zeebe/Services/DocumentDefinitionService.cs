using System.Security.Cryptography.X509Certificates;
using System;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Model.DocumentDefinitionDataModel;
using amorphie.contract.zeebe.Services.Interfaces;
using Newtonsoft.Json;

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
        // private void SetDocumentDefinitionLanguageDetail()
        // {
        //     var multiLanguageList = _documentDefinitionDataModel.data.titles.Select(x => new MultiLanguage
        //     {
        //         Name = x.title,
        //         LanguageTypeId = ZeebeMessageHelper.StringToGuid(x.language),
        //         Code = _documentdef.Code
        //     }).ToList();
        //     var multiLanguageListdb = _dbContext.MultiLanguage.Where(x => x.Code == _documentdef.Code).ToList();

        //     foreach (var i in multiLanguageList)
        //     {
        //         var multi = multiLanguageListdb.FirstOrDefault(x => x.LanguageTypeId == i.LanguageTypeId);
        //         if (multi != null)
        //         {
        //             multi.Name = i.Name;
        //             _dbContext.MultiLanguage.Update(multi);
        //         }
        //         else
        //         {
        //             _dbContext.MultiLanguage.Add(i);
        //             _documentdef.DocumentDefinitionLanguageDetails.Add(new DocumentDefinitionLanguageDetail
        //             {
        //                 DocumentDefinitionId = _documentdef.Id,
        //                 MultiLanguageId = i.Id
        //             });
        //         }
        //     }
        //     // _dbContext.SaveChanges();//gerek yok bence

        // }
        // private void SetDocumentTagsDetails()
        // {
        //     var list = _documentDefinitionDataModel.data.tags.Select(
        //                    x => new DocumentTagsDetail
        //                    {
        //                        DocumentDefinitionId = _documentdef.Id,
        //                        Tags = new core.Entity.Common.Tag
        //                        {
        //                            Contact = x.Contact,
        //                            Code = x.tag
        //                        }
        //                    }
        //                ).ToList();

        //     var tagsListCode = list.Select(x => x.Tags.Code).ToList();
        //     var tagListDb = _dbContext.Tag.Where(x => tagsListCode.Contains(x.Code));

        //     foreach (var i in list)
        //     {
        //         var tag = tagListDb.FirstOrDefault(x => x.Code == i.Tags.Code && x.Contact == i.Tags.Contact);
        //         if (tag == null)
        //         {
        //             // _dbContext.Tag.Add(i.Tags);
        //             _documentdef.DocumentTagsDetails.Add(new DocumentTagsDetail
        //             {
        //                 DocumentDefinitionId = _documentdef.Id,
        //                 Tags = i.Tags
        //             });
        //         }
        //         else
        //         {
        //             if (!_dbContext.DocumentTagsDetail.Any(x => x.TagId == tag.Id))
        //             {
        //                 _documentdef.DocumentTagsDetails.Add(new DocumentTagsDetail
        //                 {
        //                     DocumentDefinitionId = _documentdef.Id,
        //                     TagId = tag.Id
        //                 });
        //             }
        //         }
        //     }
        //     // _dbContext.SaveChanges();
        // }
        // private void SetDocumentOperations()
        // {

        //     var list = _documentDefinitionDataModel.data.optimize.Select(x => new DocumentOptimize
        //     {

        //         DocumentOptimizeType = new DocumentOptimizeType
        //         {
        //             Id = ZeebeMessageHelper.StringToGuid(x.transformto)
        //         },
        //         Size = x.size,
        //         DocumentDefinitionId = _documentdef.Id
        //     }).First();


        //     var documentOptimizeTypedb = _dbContext.DocumentOptimizeType.FirstOrDefault(x => list.DocumentOptimizeType.Id == x.Id);
        //     if (documentOptimizeTypedb != null)
        //     {
        //         if (!_dbContext.DocumentOptimize.Any(x => x.DocumentDefinitionId == _documentdef.Id && x.DocumentOptimizeTypeId == documentOptimizeTypedb.Id))
        //         {
        //             _documentdef.DocumentOptimize =
        //                 new DocumentOptimize
        //                 {
        //                     DocumentOptimizeTypeId = documentOptimizeTypedb.Id,
        //                     Size = list.Size,
        //                     DocumentDefinitionId = _documentdef.Id
        //                 };
        //         }
        //     }
        //     else
        //     {
        //         _dbContext.DocumentOptimizeType.Add(list.DocumentOptimizeType);

        //         _documentdef.DocumentOptimize =
        //                new DocumentOptimize
        //                {

        //                    DocumentOptimizeType = list.DocumentOptimizeType,
        //                    Size = list.Size,
        //                    DocumentDefinitionId = _documentdef.Id
        //                };

        //     }
        //     // _dbContext.SaveChanges();

        // }
        // private void SetDocumentOptimize()
        // {
        //     var manuelControl = _documentDefinitionDataModel.data.documentManuelControl;

        //     var list = _documentDefinitionDataModel.data.TagsOperation.Select(x => new DocumentOperations
        //     {
        //         DocumentDefinitionId = _documentdef.Id,
        //         DocumentManuelControl = manuelControl,

        //     }).First();
        //     var list2 = _documentDefinitionDataModel.data.TagsOperation.Select(x => new DocumentOperationsTagsDetail
        //     {
        //         Tags = new core.Entity.Common.Tag
        //         {
        //             Code = x.tag,
        //             Contact = x.Contact
        //         }
        //     }).ToList();
        //     list.DocumentOperationsTagsDetail = list2;
        //     _documentdef.DocumentOperations =
        //                 new DocumentOperations
        //                 {
        //                     DocumentDefinitionId = _documentdef.Id,
        //                     DocumentManuelControl = manuelControl,
        //                     DocumentOperationsTagsDetail = new List<DocumentOperationsTagsDetail>()
        //                     // TagId = tagDb.Id
        //                 };
        //     foreach (var i in list.DocumentOperationsTagsDetail)
        //     {
        //         var tag = _dbContext.Tag.FirstOrDefault(x => x.Contact == i.Tags.Contact && x.Code == i.Tags.Code);
        //         if (tag != null)
        //         {
        //             _documentdef.DocumentOperations.DocumentOperationsTagsDetail.Add(new DocumentOperationsTagsDetail
        //             {
        //                 TagId = tag.Id,
        //                 DocumentOperationsId = _documentdef.DocumentOperationsId

        //             });
        //         }
        //         else
        //         {
        //             _documentdef.DocumentOperations.DocumentOperationsTagsDetail.Add(new DocumentOperationsTagsDetail
        //             {
        //                 Tags = i.Tags,
        //                 DocumentOperationsId = _documentdef.DocumentOperationsId

        //             });
        //         }
        //     }

        // }
        // private void SetDocumentEOV()
        // {
        //     var ep = _documentDefinitionDataModel.data.EntityProperty.Select(x => new amorphie.contract.core.Entity.EAV.EntityProperty
        //     {
        //         EntityPropertyType = new core.Entity.EAV.EntityPropertyType
        //         {
        //             Code = "string"
        //         },
        //         EntityPropertyValue = new core.Entity.EAV.EntityPropertyValue { Data = x.value },
        //         Code = x.property
        //     }).ToList();
        //     var epdb = new amorphie.contract.core.Entity.EAV.EntityProperty();
        //     var entityPropertyType = _dbContext.EntityPropertyType.FirstOrDefault(x => x.Code == "string");
        //     if (entityPropertyType == null)
        //     {
        //         entityPropertyType = new core.Entity.EAV.EntityPropertyType { Code = "string" };
        //     }
        //     foreach (var i in ep)
        //     {
        //         var epdbf = _dbContext.EntityProperty.FirstOrDefault(x => x.Code == i.Code);
        //         if (epdbf != null)
        //         {
        //             epdb = epdbf;
        //         }
        //         epdb.Code = i.Code;

        //         var epv = _dbContext.EntityPropertyValue.FirstOrDefault(x => x.Data == i.EntityPropertyValue.Data);
        //         if (epv != null)
        //         {
        //             epdb.EntityPropertyValue = epv;
        //         }
        //         else
        //         {
        //             epdb.EntityPropertyValue = i.EntityPropertyValue;
        //         }
        //         epdb.EntityPropertyTypeId = entityPropertyType.Id;
        //         var dep = _dbContext.DocumentEntityProperty.FirstOrDefault(x => x.EntityProperty.Code == i.Code);
        //         if (dep == null)
        //         {
        //             _documentdef.DocumentEntityPropertys.Add(new DocumentEntityProperty
        //             {
        //                 DocumentDefinitionId = _documentdef.Id,
        //                 EntityProperty = epdb,
        //             });
        //         }
        //     }
        // }
        // private void SetDocumentAllowedClientDetail()
        // {
        //     var allowedClientDetail = _documentDefinitionDataModel.data.allowedclients.Select(x => new DocumentAllowedClientDetail
        //     {
        //         DocumentDefinitionId = _documentdef.Id,
        //         DocumentAllowedClientId = ZeebeMessageHelper.StringToGuid(x.select)

        //     }).ToList();
        //     if (_documentdef.DocumentUpload == null)
        //     {
        //         _documentdef.DocumentUpload = new DocumentUpload();
        //     }
        //     foreach (var i in allowedClientDetail)
        //     {
        //         var dacd = _dbContext.DocumentAllowedClientDetail.FirstOrDefault(x => x.DocumentDefinitionId == i.DocumentDefinitionId && x.DocumentAllowedClientId == i.DocumentAllowedClientId);
        //         if (dacd == null)
        //         {
        //             _documentdef.DocumentUpload.DocumentAllowedClientDetails.Add(new DocumentAllowedClientDetail
        //             {
        //                 DocumentDefinitionId = _documentdef.Id,
        //                 DocumentAllowedClientId = i.DocumentAllowedClientId
        //             });
        //         }
        //     }
        // }

        // private void SetDocumentFormatDetail()
        // {
        //     var documentFormatDetail = _documentDefinitionDataModel.data.allowedformats.Select(x => new DocumentFormatDetail
        //     {
        //         DocumentDefinitionId = _documentdef.Id,
        //         DocumentFormat = new DocumentFormat
        //         {
        //             DocumentFormatTypeId = ZeebeMessageHelper.StringToGuid(x.format),
        //             DocumentSizeId = ZeebeMessageHelper.StringToGuid(x.maxsizekilobytes)
        //         }
        //     }).ToList();
        //     if (_documentdef.DocumentUpload == null)
        //     {
        //         _documentdef.DocumentUpload = new DocumentUpload();
        //     }

        //     foreach (var i in documentFormatDetail)
        //     {
        //         var dacd = _dbContext.DocumentFormatDetail.FirstOrDefault(x => x.DocumentDefinitionId == i.DocumentDefinitionId
        //          && x.DocumentFormat.DocumentFormatTypeId == i.DocumentFormat.DocumentFormatTypeId
        //           && x.DocumentFormat.DocumentSizeId == i.DocumentFormat.DocumentSizeId);
        //         if (dacd == null)
        //         {
        //             _documentdef.DocumentUpload.DocumentFormatDetails.Add(i);
        //         }
        //     }
        //     // _dbContext.SaveChanges();

        // }
        // private void SetDocumentUpload()
        // {
        //     if (_documentdef.DocumentUpload == null)
        //     {
        //         _documentdef.DocumentUpload = new DocumentUpload();
        //     }
        //     _documentdef.DocumentUpload.Required = _documentDefinitionDataModel.data.scarequired;


        // }

        private void DynamicToDocumentDefinitionDataModel()
        {
            _documentDefinitionDataModel = new DocumentDefinitionDataModel();
            _documentDefinitionDataModel.data = new Data();
            _documentDefinitionDataModel.data = JsonConvert.DeserializeObject<Data>(_documentDefinitionDataDynamic);
        }

        public async Task<DocumentDefinition> DataModelToDocumentDefinition(dynamic? documentDefinitionDataDynamic, Guid id)
        {
            _documentDefinitionDataDynamic = documentDefinitionDataDynamic;
            try
            {
                DynamicToDocumentDefinitionDataModel();
                var onHoldStatus = _dbContext.Status.FirstOrDefault(x=>x.Code == "on-hold");
                if(onHoldStatus==null){
                    onHoldStatus =new Status{Code="on-hold"};
                }
                var documentDefinition = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == _documentDefinitionDataModel.data.Code);
                if (documentDefinition != null)
                {
                    _documentdef = documentDefinition;
                }
                else
                {
                    _documentdef = new core.Entity.Document.DocumentDefinition
                    {
                        Id = id,
                        Code = _documentDefinitionDataModel.data.Code,
                        Status = onHoldStatus,
                        BaseStatus = onHoldStatus
                    };
                }

                // SetDocumentDefinitionLanguageDetail();
                // SetDocumentTagsDetails();
                // SetDocumentOperations();
                // SetDocumentOptimize();
                // SetDocumentAllowedClientDetail();
                // SetDocumentFormatDetail();
                // SetDocumentUpload();
                // SetDocumentEOV();
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