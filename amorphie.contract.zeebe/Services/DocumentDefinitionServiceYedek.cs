// using System.Linq;
// using System.Security.Cryptography.X509Certificates;
// using System;
// using amorphie.contract.core.Entity.Common;
// using amorphie.contract.core.Entity.Document;
// using amorphie.contract.core.Entity.Document.DocumentTypes;
// using amorphie.contract.data.Contexts;
// using amorphie.contract.zeebe.Model.DocumentDefinitionDataModel;
// using amorphie.contract.zeebe.Services.Interfaces;
// using Newtonsoft.Json;
// using Microsoft.EntityFrameworkCore;

// namespace amorphie.contract.zeebe.Services
// {
//     public class DocumentDefinitionService : IDocumentDefinitionService
//     {
//         ProjectDbContext _dbContext;
//         DocumentDefinitionDataModel _documentDefinitionDataModel;
//         DocumentDefinition _documentdef;
//         dynamic? _documentDefinitionDataDynamic;
//         public DocumentDefinitionService(ProjectDbContext dbContext)
//         {
//             _dbContext = dbContext;
//         }


//         private void SetDocumentDefinitionLanguageDetail()
//         {
//             var multiLanguageList = _documentDefinitionDataModel.data.Titles.Select(x => new MultiLanguage
//             {
//                 Name = x.title,
//                 LanguageTypeId = ZeebeMessageHelper.StringToGuid(x.language),
//                 Code = _documentdef.Code
//             }).ToList();

//             _documentdef.DocumentDefinitionLanguageDetails = multiLanguageList.Select(x => new DocumentDefinitionLanguageDetail
//             {
//                 DocumentDefinitionId = _documentdef.Id,
//                 MultiLanguage = x
//             }).ToList();
//             // var multiLanguageListdb = _dbContext.MultiLanguage.Where(x => x.Code == _documentdef.Code).ToList();

//             // foreach (var i in multiLanguageList)
//             // {
//             //     var multi = multiLanguageListdb.FirstOrDefault(x => x.LanguageTypeId == i.LanguageTypeId);
//             //     if (multi != null)
//             //     {
//             //         multi.Name = i.Name;
//             //         _dbContext.MultiLanguage.Update(multi);
//             //     }
//             //     else
//             //     {
//             //         _documentdef.DocumentDefinitionLanguageDetails.Add(new DocumentDefinitionLanguageDetail
//             //         {
//             //             DocumentDefinitionId = _documentdef.Id,
//             //             MultiLanguage = i
//             //         });
//             //     }
//             // }

//         }
//         private void SetDocumentTagsDetails()
//         {
//             var list = _documentDefinitionDataModel.data.Tags.Select(
//                            x => new DocumentTagsDetail
//                            {
//                                DocumentDefinitionId = _documentdef.Id,
//                                TagId = ZeebeMessageHelper.StringToGuid(x)
//                            }
//                        ).ToList();
//             _documentdef.DocumentTagsDetails = list;
//             // var tagidList = list.Select(x => x.TagId).ToList();

//             // var dbDtd = _dbContext.DocumentTagsDetail.Where(x => tagidList.Contains(x.TagId) && x.DocumentDefinitionId == _documentdef.Id).ToList();

//             // foreach (var i in list)
//             // {
//             //     if (!dbDtd.Any(x => x.TagId == i.TagId))
//             //     {
//             //         _documentdef.DocumentTagsDetails.Add(new DocumentTagsDetail
//             //         {
//             //             DocumentDefinitionId = _documentdef.Id,
//             //             TagId = i.TagId
//             //         });
//             //     }
//             // }
//         }
//         private void SetDocumentOptimize()
//         {
//             _documentdef.DocumentOptimize = new DocumentOptimize
//             {
//                 DocumentOptimizeType = new DocumentOptimizeType
//                 {
//                     Id = ZeebeMessageHelper.StringToGuid(_documentDefinitionDataModel.data.TransformTo)
//                 },
//                 Size = _documentDefinitionDataModel.data.Size,
//                 DocumentDefinitionId = _documentdef.Id
//             };

//             // var documentOptimizeTypedb = _dbContext.DocumentOptimizeType.FirstOrDefault(x => list.DocumentOptimizeType.Id == x.Id);
//             // if (documentOptimizeTypedb != null)
//             // {
//             //     if (!_dbContext.DocumentOptimize.Any(x => x.DocumentDefinitionId == _documentdef.Id && x.DocumentOptimizeTypeId == documentOptimizeTypedb.Id))
//             //     {
//             //         _documentdef.DocumentOptimize =
//             //             new DocumentOptimize
//             //             {
//             //                 DocumentOptimizeTypeId = documentOptimizeTypedb.Id,
//             //                 Size = list.Size,
//             //                 DocumentDefinitionId = _documentdef.Id
//             //             };
//             //     }
//             // }
//             // else
//             // {
//             //     _dbContext.DocumentOptimizeType.Add(list.DocumentOptimizeType);

//             //     _documentdef.DocumentOptimize =
//             //            new DocumentOptimize
//             //            {

//             //                DocumentOptimizeType = list.DocumentOptimizeType,
//             //                Size = list.Size,
//             //                DocumentDefinitionId = _documentdef.Id
//             //            };

//             // }

//         }
//         private void SetDocumentOperation()
//         {
//             var manuelControl = _documentDefinitionDataModel.data.DocumentManuelControl;


//             var list2 = _documentDefinitionDataModel.data.UploadTags.Select(x => new DocumentOperationsTagsDetail
//             {
//                 TagId = ZeebeMessageHelper.StringToGuid(x),
//             }).ToList();
//             _documentdef.DocumentOperations =
//                         new DocumentOperations
//                         {
//                             DocumentDefinitionId = _documentdef.Id,
//                             DocumentManuelControl = manuelControl,
//                             DocumentOperationsTagsDetail = list2
//                         };
//             // foreach (var i in list.DocumentOperationsTagsDetail)
//             // {
//             //     var tag = _dbContext.Tag.FirstOrDefault(x => x.Contact == i.Tags.Contact && x.Code == i.Tags.Code);
//             //     if (tag != null)
//             //     {
//             //         _documentdef.DocumentOperations.DocumentOperationsTagsDetail.Add(new DocumentOperationsTagsDetail
//             //         {
//             //             TagId = tag.Id,
//             //             DocumentOperationsId = _documentdef.DocumentOperationsId

//             //         });
//             //     }
//             //     else
//             //     {
//             //         _documentdef.DocumentOperations.DocumentOperationsTagsDetail.Add(new DocumentOperationsTagsDetail
//             //         {
//             //             Tags = i.Tags,
//             //             DocumentOperationsId = _documentdef.DocumentOperationsId

//             //         });
//             //     }
//             // }

//         }
//         private void SetDocumentEOV()
//         {
//             var ep = _documentDefinitionDataModel.data.EntityProperty.Select(x => new amorphie.contract.core.Entity.EAV.EntityProperty
//             {
//                 EntityPropertyType = new core.Entity.EAV.EntityPropertyType
//                 {
//                     Code = "string"
//                 },
//                 EntityPropertyValue = new core.Entity.EAV.EntityPropertyValue { Data = x.value },
//                 Code = x.property
//             }).ToList();
//             var epdb = new amorphie.contract.core.Entity.EAV.EntityProperty();
// #region EntityPropertyType Control
//             var entityPropertyType = _dbContext.EntityPropertyType.FirstOrDefault(x => x.Code == "string");

//             if (entityPropertyType == null)
//             {
//                 entityPropertyType = new core.Entity.EAV.EntityPropertyType { Code = "string" };
//             }
// #endregion

//             foreach (var i in ep)
//             {
//                 var epdbf = _dbContext.EntityProperty.FirstOrDefault(x => x.Code == i.Code);
//                 if (epdbf != null)
//                 {
//                     epdb = epdbf;
//                 }
//                 epdb.Code = i.Code;

//                 var epv = _dbContext.EntityPropertyValue.FirstOrDefault(x => x.Data == i.EntityPropertyValue.Data);
//                 if (epv != null)
//                 {
//                     epdb.EntityPropertyValue = epv;
//                 }
//                 else
//                 {
//                     epdb.EntityPropertyValue = i.EntityPropertyValue;
//                 }
//                 epdb.EntityPropertyTypeId = entityPropertyType.Id;
//                 var dep = _dbContext.DocumentEntityProperty.FirstOrDefault(x => x.EntityProperty.Code == i.Code);
//                 if (dep == null)
//                 {
//                     _documentdef.DocumentEntityPropertys.Add(new DocumentEntityProperty
//                     {
//                         DocumentDefinitionId = _documentdef.Id,
//                         EntityProperty = epdb,
//                     });
//                 }
//             }
//         }
//         private void SetDocumentAllowedClientDetail()
//         {
//             var allowedClientDetail = _documentDefinitionDataModel.data.UploadAllowedClients.Select(x => new DocumentAllowedClientDetail
//             {
//                 DocumentDefinitionId = _documentdef.Id,
//                 DocumentAllowedClientId = ZeebeMessageHelper.StringToGuid(x.Key)

//             }).ToList();
//             if (_documentdef.DocumentUpload == null)
//             {
//                 _documentdef.DocumentUpload = new DocumentUpload();
//             }
//             _documentdef.DocumentUpload.DocumentAllowedClientDetails = allowedClientDetail;
//             // foreach (var i in allowedClientDetail)
//             // {
//             //     var dacd = _dbContext.DocumentAllowedClientDetail.FirstOrDefault(x => x.DocumentDefinitionId == i.DocumentDefinitionId && x.DocumentAllowedClientId == i.DocumentAllowedClientId);
//             //     if (dacd == null)
//             //     {
//             //         _documentdef.DocumentUpload.DocumentAllowedClientDetails.Add(new DocumentAllowedClientDetail
//             //         {
//             //             DocumentDefinitionId = _documentdef.Id,
//             //             DocumentAllowedClientId = i.DocumentAllowedClientId
//             //         });
//             //     }
//             // }
//         }

//         private void SetDocumentFormatDetail()
//         {
//             var documentFormatDetail = _documentDefinitionDataModel.data.AllowedFormatsUploadList.Select(x => new DocumentFormatDetail
//             {
//                 DocumentDefinitionId = _documentdef.Id,
//                 DocumentFormat = new DocumentFormat
//                 {
//                     DocumentFormatTypeId = ZeebeMessageHelper.StringToGuid(x.format),
//                     DocumentSizeId = ZeebeMessageHelper.StringToGuid(x.maxsizekilobytes)
//                 }
//             }).ToList();
//             if (_documentdef.DocumentUpload == null)
//             {
//                 _documentdef.DocumentUpload = new DocumentUpload();
//             }
//             _documentdef.DocumentUpload.DocumentFormatDetails = documentFormatDetail;
//             // foreach (var i in documentFormatDetail)
//             // {
//             //     var dacd = _dbContext.DocumentFormatDetail.FirstOrDefault(x => x.DocumentDefinitionId == i.DocumentDefinitionId
//             //      && x.DocumentFormat.DocumentFormatTypeId == i.DocumentFormat.DocumentFormatTypeId
//             //       && x.DocumentFormat.DocumentSizeId == i.DocumentFormat.DocumentSizeId);
//             //     if (dacd == null)
//             //     {
//             //         _documentdef.DocumentUpload.DocumentFormatDetails.Add(i);
//             //     }
//             // }

//         }
//         private void SetDocumentUpload()
//         {
//             if (_documentdef.DocumentUpload == null)
//             {
//                 _documentdef.DocumentUpload = new DocumentUpload();
//             }
//             _documentdef.DocumentUpload.Required = _documentDefinitionDataModel.data.ScaRequired;
//             SetDocumentAllowedClientDetail();
//             SetDocumentFormatDetail();

//         }

//         private void DynamicToDocumentDefinitionDataModel()
//         {
//             _documentDefinitionDataModel = JsonConvert.DeserializeObject<DocumentDefinitionDataModel>(_documentDefinitionDataDynamic);
//         }


//         public async Task<DocumentDefinition> DataModelToDocumentDefinition(dynamic documentDefinitionDataDynamic, Guid id)
//         {
//             _documentDefinitionDataDynamic = documentDefinitionDataDynamic;
//             try
//             {
//                 DynamicToDocumentDefinitionDataModel();
//                 var onHoldStatus = _dbContext.Status.FirstOrDefault(x => x.Code == "on-hold");
//                 if (onHoldStatus == null)
//                 {
//                     onHoldStatus = new Status { Code = "on-hold" };
//                 }
//                 var documentDefinition = _dbContext.DocumentDefinition.FirstOrDefault(x => x.Code == _documentDefinitionDataModel.data.Code);
//                 if (documentDefinition != null)
//                 {
//                     _documentdef = documentDefinition;
//                 }
//                 else
//                 {
//                     // _documentdef = new DocumentDefinition
//                     // {
//                     //     Id = id,
//                     //     Code = _documentDefinitionDataModel.data.Code,
//                     //     Status = onHoldStatus,
//                     //     BaseStatus = onHoldStatus
//                     // };
//                 }

//                 SetDocumentDefinitionLanguageDetail();
//                 SetDocumentTagsDetails();
//                 SetDocumentOptimize();
//                 SetDocumentOperation();
//                 if (_documentDefinitionDataModel.data.DocumentType.IndexOf("Upload") > -1)
//                 {
//                     SetDocumentUpload();
//                 }

//                 SetDocumentEOV();
//                 _dbContext.DocumentDefinition.Add(_documentdef);
//                 _dbContext.SaveChanges();
//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }
//             return _documentdef;

//         }

//     }
// }