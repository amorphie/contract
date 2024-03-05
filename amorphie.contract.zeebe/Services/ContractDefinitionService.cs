using amorphie.contract.core.Entity.Common;
using amorphie.contract.infrastructure.Contexts;
using Newtonsoft.Json;
using amorphie.contract.zeebe.Model.ContractDefinitionDataModel;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Enum;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Entity.EAV;
using System.Collections;

namespace amorphie.contract.zeebe.Services
{
    public interface IContractDefinitionService
    {
        Task<ContractDefinition> DataModelToContractDefinition(dynamic contractDefinitionDataDynamic, Guid id);
        Task<ContractDefinition> DataModelToContractDefinitionUpdate(dynamic contractDefinitionDataDynamic, Guid id);
    }
    public class ContractDefinitionService : IContractDefinitionService
    {
        ProjectDbContext _dbContext;
        ContractDefinitionDataModel _ContractDefinitionDataModel;
        ContractDefinition _ContractDefinition;
        dynamic? _ContractDefinitionDataModelDynamic;
        public ContractDefinitionService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private void SetContractDefinitionDefault(Guid id)
        {
            _ContractDefinition = new ContractDefinition();
            var activeStatus = EStatus.Active;

            _ContractDefinition.Id = id;
            _ContractDefinition.Status = activeStatus;
            _ContractDefinition.Code = _ContractDefinitionDataModel.code;

        }
        private void DynamicToContractDefinitionDataModel()
        {
            var test = _ContractDefinitionDataModelDynamic.ToString().Replace("{}", "\"\"");
            _ContractDefinitionDataModel = new ContractDefinitionDataModel();
            _ContractDefinitionDataModel = JsonConvert.DeserializeObject<ContractDefinitionDataModel>
            (test.Replace("{}", "\"\""));
        }
        private void UpdateContractDefinitionLanguage(List<MultiLanguage> multiLanguages)
        {
            var removedMultiLang = multiLanguages.Where(x => !_ContractDefinitionDataModel.Titles.Any(y => ZeebeMessageHelper.StringToGuid(y.language) == x.LanguageTypeId)).ToList();
            var addedMultiLang = _ContractDefinitionDataModel.Titles
                                .Where(x => !multiLanguages.Any(y => y.LanguageTypeId == ZeebeMessageHelper.StringToGuid(x.language)))
                                .Select(x => new ContractDefinitionLanguageDetail
                                {
                                    ContractDefinitionId = _ContractDefinition.Id,
                                    MultiLanguage = new MultiLanguage
                                    {
                                        Name = x.title,
                                        LanguageTypeId = ZeebeMessageHelper.StringToGuid(x.language),
                                        Code = _ContractDefinition.Code
                                    }
                                })
                                .ToList();

            foreach (var oldMultiLanguage in multiLanguages)
            {
                var matchingMultiLang = _ContractDefinitionDataModel.Titles.FirstOrDefault(y => ZeebeMessageHelper.StringToGuid(y.language) == oldMultiLanguage.LanguageTypeId);
                if (matchingMultiLang != null)
                {
                    oldMultiLanguage.Name = matchingMultiLang.title;
                }
            }
            if (removedMultiLang.Any())
            {
                _dbContext.RemoveRange(removedMultiLang);
            }

            if (addedMultiLang.Any())
            {
                _dbContext.AddRange(addedMultiLang);
            }
        }
        private void SetContractDefinitionLanguageDetail()
        {
            var multiLanguageList = _ContractDefinitionDataModel.Titles.Select(x => new MultiLanguage
            {
                Name = x.title,
                LanguageTypeId = ZeebeMessageHelper.StringToGuid(x.language),
                Code = _ContractDefinition.Code
            }).ToList();

            _ContractDefinition.ContractDefinitionLanguageDetails = multiLanguageList.Select(x => new ContractDefinitionLanguageDetail
            {
                ContractDefinitionId = _ContractDefinition.Id,
                MultiLanguage = x
            }).ToList();
        }

        private void UpdateContractDocumentGroupDetail(List<ContractDocumentGroupDetail> contractDocumentGroupDetails)
        {
            if (_ContractDefinitionDataModel.documentGroupList.Any(x => x.groupName != null && x.groupName?.id != ""))
            {
                var removedItems = contractDocumentGroupDetails.Where(x => !_ContractDefinitionDataModel.documentGroupList.Any(y => x.DocumentGroupId == ZeebeMessageHelper.StringToGuid(y.groupName.id))).ToList();
                var addedItems = _ContractDefinitionDataModel.documentGroupList.Where(x => !contractDocumentGroupDetails.Any(y => ZeebeMessageHelper.StringToGuid(x.groupName.id) == y.DocumentGroupId))
                                .Select(x => new ContractDocumentGroupDetail
                                {
                                    ContractDefinitionId = _ContractDefinition.Id,
                                    DocumentGroupId = ZeebeMessageHelper.StringToGuid(x.groupName.id),
                                    AtLeastRequiredDocument = x.atLeastRequiredDocument,
                                    Required = x.required
                                }).ToList();
                if (removedItems.Any())
                {
                    _dbContext.RemoveRange(removedItems);
                }

                if (addedItems.Any())
                {
                    _dbContext.AddRange(addedItems);
                }
            }
        }
        private void SetContractDocumentGroupDetail()
        {
            if (_ContractDefinitionDataModel.documentGroupList.Any(x => x.groupName?.id != "" && x.groupName != null))
            {
                var contractDocumentGroupDetails = _ContractDefinitionDataModel.documentGroupList?.Select(x => new ContractDocumentGroupDetail
                {
                    ContractDefinitionId = _ContractDefinition.Id,
                    DocumentGroupId = ZeebeMessageHelper.StringToGuid(x.groupName.id),
                    AtLeastRequiredDocument = x.atLeastRequiredDocument,
                    Required = x.required
                });
                _ContractDefinition.ContractDocumentGroupDetails = contractDocumentGroupDetails.ToList();
            }
        }

        private void UpdateContractDocumentDetail(List<ContractDocumentDetail> contractDocumentDetails)
        {
            var removedItems = contractDocumentDetails.Where(x => !_ContractDefinitionDataModel.documentsList
                            .Any(y => x.DocumentDefinition.Code == y.name.code && x.DocumentDefinition.Semver == y.minVersiyon)).ToList();
            var addedItems = _ContractDefinitionDataModel.documentsList.Where(x => !contractDocumentDetails
                            .Any(y => x.minVersiyon == y.DocumentDefinition.Semver && x.name.code == y.DocumentDefinition.Code))
                            .Select(x => new ContractDocumentDetail
                            {
                                ContractDefinitionId = _ContractDefinition.Id,
                                DocumentDefinitionId = _dbContext.DocumentDefinition.Where(y => y.Semver == x.minVersiyon && y.Code == x.name.code).Select(y => y.Id).FirstOrDefault(),
                                UseExisting = x.useExisting,
                                Required = x.required
                            }).ToList();
            if (removedItems.Any())
            {
                _dbContext.RemoveRange(removedItems);
            }

            if (addedItems.Any())
            {
                _dbContext.AddRange(addedItems);
            }
        }
        private void SetContractDocumentDetail()
        {
            var contractDocumentDetail = _ContractDefinitionDataModel.documentsList.Select(x => new ContractDocumentDetail
            {
                ContractDefinitionId = _ContractDefinition.Id,
                DocumentDefinitionId = _dbContext.DocumentDefinition.Where(y => y.Semver == x.minVersiyon && y.Code == x.name.code).Select(y => y.Id).FirstOrDefault(),
                UseExisting = x.useExisting,
                Required = x.required
            });
            _ContractDefinition.ContractDocumentDetails = contractDocumentDetail.ToList();

        }

        private void UpdateContractTag(List<ContractTag> contractTags)
        {
            var removedItems = contractTags.Where(x => !_ContractDefinitionDataModel.tags.Any(y => ZeebeMessageHelper.StringToGuid(y) == x.TagId)).ToList();
            var addedItems = _ContractDefinitionDataModel.tags.Where(x => !contractTags.Any(y => y.TagId == ZeebeMessageHelper.StringToGuid(x)))
                            .Select(x => new ContractTag
                            {
                                ContractDefinitionId = _ContractDefinition.Id,
                                TagId = ZeebeMessageHelper.StringToGuid(x)
                            }).ToList();
            if (removedItems.Any())
            {
                _dbContext.RemoveRange(removedItems);
            }

            if (addedItems.Any())
            {
                _dbContext.AddRange(addedItems);
            }
        }

        private void SetContractTag()
        {
            var list = _ContractDefinitionDataModel.tags.Select(
                           x => new ContractTag
                           {
                               ContractDefinitionId = _ContractDefinition.Id,
                               TagId = ZeebeMessageHelper.StringToGuid(x)
                           }
                       ).ToList();
            _ContractDefinition.ContractTags = list;
        }
        private void SetContractEntityProperty()
        {
            if (_ContractDefinitionDataModel.EntityProperty == null)
            {
                return;
            }
            var ep = _ContractDefinitionDataModel.EntityProperty.Select(x => new amorphie.contract.core.Entity.EAV.EntityProperty
            {
                EEntityPropertyType = (ushort)EEntityPropertyType.str,
                EntityPropertyValue = new core.Entity.EAV.EntityPropertyValue { Data = x.value },
                Code = x.PropertyName
            }).ToList();
            var epdb = new amorphie.contract.core.Entity.EAV.EntityProperty();
            foreach (var i in ep)
            {
                epdb.Code = i.Code;
                epdb.EntityPropertyValue = i.EntityPropertyValue;
                epdb.EEntityPropertyType = (ushort)EEntityPropertyType.str;
                _ContractDefinition.ContractEntityProperty.Add(new ContractEntityProperty
                {
                    ContractDefinitionId = _ContractDefinition.Id,
                    EntityProperty = epdb,
                });
            }
        }

        private void UpdateEntityProperty(List<core.Entity.EAV.EntityProperty> oldEntityProperties)
        {
            if (_ContractDefinitionDataModel.EntityProperty == null)
            {
                return;
            }
            var removedItems = oldEntityProperties.Where(x => _ContractDefinitionDataModel.EntityProperty.Any(y => y.PropertyName == x.Code)).ToList();
            var addedItems = _ContractDefinitionDataModel.EntityProperty.Where(x => !oldEntityProperties.Any(y => y.Code == x.PropertyName))
                            .Select(x => new ContractEntityProperty
                            {
                                ContractDefinitionId = _ContractDefinition.Id,
                                EntityProperty = new core.Entity.EAV.EntityProperty
                                {
                                    EEntityPropertyType = (ushort)EEntityPropertyType.str,
                                    EntityPropertyValue = new core.Entity.EAV.EntityPropertyValue { Data = x.value },
                                    Code = x.PropertyName
                                }
                            }).ToList();
            foreach (var oldEntityProperty in oldEntityProperties)
            {
                var matchingEntityProperty = _ContractDefinitionDataModel.EntityProperty.FirstOrDefault(y => y.PropertyName == oldEntityProperty.Code);

                if (matchingEntityProperty != null)
                {
                    oldEntityProperty.EntityPropertyValue.Data = matchingEntityProperty.value;
                }
            }
            if (removedItems.Any())
            {
                _dbContext.RemoveRange(removedItems);
            }

            if (addedItems.Any())
            {
                _dbContext.AddRange(addedItems);
            }
        }

        #region VALIDATION
        // private Validation MapContractValidation()
        // {
        //     var list = _ContractDefinitionDataModel.validationList.Select(x =>
        //             {
        //                 var validationType = x.type;
        //                 var existingValidation = _dbContext.Validation.FirstOrDefault(a => a.EValidationType == validationType);
        //                 if (existingValidation != null)
        //                 {
        //                     return new ContractValidation
        //                     {
        //                         ContractDefinitionId = _ContractDefinition.Id,
        //                         ValidationId = existingValidation.Id
        //                     };
        //                 }
        //                 else
        //                 {
        //                     return new ContractValidation
        //                     {

        //                         ContractDefinitionId = _ContractDefinition.Id,
        //                         Validations = new Validation
        //                         {
        //                             EValidationType = validationType,
        //                         },
        //                     };
        //                 }

        //             }).ToList();
        //     _ContractDefinition.ContractValidations = list;
        // }
        #endregion
        private void SetContractValidation()
        {
            var list = _ContractDefinitionDataModel.validationList.Select(x =>
                    {
                        var validationType = x.type;
                        var existingValidation = _dbContext.Validation.FirstOrDefault(a => a.EValidationType == validationType);
                        if (existingValidation != null)
                        {
                            return new ContractValidation
                            {
                                ContractDefinitionId = _ContractDefinition.Id,
                                ValidationId = existingValidation.Id
                            };
                        }
                        else
                        {
                            return new ContractValidation
                            {

                                ContractDefinitionId = _ContractDefinition.Id,
                                Validations = new Validation
                                {
                                    EValidationType = validationType,
                                },
                            };
                        }

                    }).ToList();
            _ContractDefinition.ContractValidations = list;
        }
        private void SetContractBankEntity()
        {
            _ContractDefinition.BankEntity = _ContractDefinitionDataModel.registrationType;
        }
        public async Task<ContractDefinition> DataModelToContractDefinition(dynamic documentDefinitionDataModelDynamic, Guid id)
        {
            _ContractDefinitionDataModelDynamic = documentDefinitionDataModelDynamic;
            try
            {
                DynamicToContractDefinitionDataModel();
                SetContractDefinitionDefault(id);
                SetContractDefinitionLanguageDetail();
                SetContractDocumentGroupDetail();
                SetContractDocumentDetail();
                SetContractTag();
                SetContractEntityProperty();
                SetContractValidation();
                SetContractBankEntity();
                _dbContext.ContractDefinition.Add(_ContractDefinition);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ContractDefinition;

        }

        public void DifferenceCollector<T>(ICollection<T> existingItems, ICollection<T> updatedItems,
                                        Func<T, T, bool> condition) where T : class
        {
            existingItems ??= new List<T>();
            updatedItems ??= new List<T>();

            var itemsToRemove = existingItems.Where(existing => updatedItems.All(updated => !condition(existing, updated))).ToList();
            var itemsToAdd = updatedItems.Where(updated => existingItems.All(existing => !condition(existing, updated))).ToList();

            if (itemsToRemove.Any())
            {
                _dbContext.RemoveRange(itemsToRemove);
            }

            if (itemsToAdd.Any())
            {
                _dbContext.AddRange(itemsToAdd);
            }
        }
        public async Task<ContractDefinition> DataModelToContractDefinitionUpdate(dynamic contractDefinitionDataUpdateDynamic, Guid id)
        {
            _ContractDefinitionDataModelDynamic = contractDefinitionDataUpdateDynamic;
            try
            {
                DynamicToContractDefinitionDataModel();
                SetContractDefinitionDefault(id);
                var contractDefinition = _dbContext.ContractDefinition.FirstOrDefault(x => x.Id == _ContractDefinition.Id);
                if (_ContractDefinition == null)
                {
                    throw new ArgumentException("Güncellemek istediğiniz döküman bulunmamakta.");
                }

                contractDefinition.ModifiedAt = DateTime.UtcNow;
                contractDefinition.BankEntity = _ContractDefinition.BankEntity;
                UpdateContractDefinitionLanguage(contractDefinition.ContractDefinitionLanguageDetails.Select(x => x.MultiLanguage).ToList());
                UpdateContractDocumentGroupDetail(contractDefinition.ContractDocumentGroupDetails.ToList());
                UpdateContractDocumentDetail(contractDefinition.ContractDocumentDetails.ToList());
                UpdateContractTag(contractDefinition.ContractTags.ToList());
                UpdateEntityProperty(contractDefinition.ContractEntityProperty.Select(x => x.EntityProperty).ToList());
                await _dbContext.SaveChangesAsync();

                #region GENERIC_UPDATER
                // var oldEntityPropertyValueContract = contractDefinition.ContractEntityProperty.Select(x=>x.EntityProperty.EntityPropertyValue).ToList();
                // var newEntityPropertValueContract = MapEntityPropertyValue();
                // Func<EntityPropertyValue,EntityPropertyValue,bool> conditionEntityPropertyValue = (exist,update) => exist.Data == update.Data;
                // var entityPropertyValue = UpdateRelationships(oldEntityPropertyValueContract,newEntityPropertValueContract,conditionEntityPropertyValue);
                // if(entityPropertyValue.Any())
                // {
                //     var oldEntityPropertyContract = contractDefinition.ContractEntityProperty.Select(x=>x.EntityProperty).ToList();
                //     var newEntityPropertyContract = MapEntityProperty(entityPropertyValue);
                //     Func<core.Entity.EAV.EntityProperty,core.Entity.EAV.EntityProperty,bool> conditionEntityProperty = (exist,update) => exist.Code == update.Code;
                //     UpdateRelationships(oldEntityPropertyContract,newEntityPropertyContract,conditionEntityProperty);
                // }

                // // TAG UPDATE
                // var oldTags = contractDefinition.ContractTags;
                // var newTags = MapContractTag();
                // Func<ContractTag, ContractTag, bool> conditionTag = (exist,update) => exist.TagId == update.TagId;
                // UpdateRelationships(oldTags,newTags,conditionTag);
                // // VALIDATION UPDATE

                // // DOCUMENT LIST UPDATE
                // var oldDocs = contractDefinition.ContractDocumentDetails;
                // var newDocs = MapContractDocumentDetail();
                // Func<ContractDocumentDetail, ContractDocumentDetail, bool> conditionDocDetail = (exist,update) => exist.DocumentDefinitionId == update.DocumentDefinitionId;
                // UpdateRelationships(oldDocs,newDocs,conditionDocDetail);

                // // DOCUMENT GROUP LIST UPDATE
                // var oldDocGroup = contractDefinition.ContractDocumentGroupDetails;
                // var newDocGroup = MapContractDocumentGroupDetail();
                // Func<ContractDocumentGroupDetail,ContractDocumentGroupDetail,bool> conditionDocGrupDetail = (exist,update) => exist.DocumentGroupId == update.DocumentGroupId;
                // UpdateRelationships(oldDocGroup,newDocGroup,conditionDocGrupDetail);

                // //TITLE UPDATE
                // var oldMultiLang = contractDefinition.ContractDefinitionLanguageDetails.Select(x=>x.MultiLanguage).ToList();
                // var newMultiLang = MapContractDefinitionLanguage();
                // Func<MultiLanguage,MultiLanguage,bool> conditionMultiLanguage = (exist,update) => exist.Code == update.Code && exist.LanguageType == update.LanguageType;
                // UpdateRelationships(oldMultiLang,newMultiLang,conditionMultiLanguage);

                // var oldMultiLangDetail = contractDefinition.ContractDefinitionLanguageDetails;
                // var newMultiLangDetail = MapContractDefinitionLanguageDetail(newMultiLang); // BİR SIKINTI OLABİLİR
                // Func<ContractDefinitionLanguageDetail,ContractDefinitionLanguageDetail,bool> conditionMultiLanguageDetail = (exist,update) => exist.MultiLanguageId == update.MultiLanguageId;
                // UpdateRelationships(oldMultiLangDetail,newMultiLangDetail,conditionMultiLanguageDetail);

                //VALIDATION UPDATE
                //var contractValidation = _ContractDefinitionDataModel.validationList.First();
                //var newValidationDecision = MapValidationDecision(_ContractDefinitionDataModel.validationList.First());
                #endregion


            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return _ContractDefinition;
        }
    }
}