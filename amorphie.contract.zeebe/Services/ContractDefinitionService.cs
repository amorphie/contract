using amorphie.contract.core.Entity.Common;
using amorphie.contract.data.Contexts;
using Newtonsoft.Json;
using amorphie.contract.zeebe.Model.ContractDefinitionDataModel;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Enum;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Entity.EAV;

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

        private List<MultiLanguage> MapContractDefinitionLanguage()
        {
            var multiLanguageList = _ContractDefinitionDataModel.Titles.Select(x => new MultiLanguage
            {
                Name = x.title,
                LanguageTypeId = ZeebeMessageHelper.StringToGuid(x.language),
                Code = _ContractDefinition.Code
            }).ToList();
            return multiLanguageList;
        }
        
        private List<ContractDefinitionLanguageDetail> MapContractDefinitionLanguageDetail(List<MultiLanguage> multiLanguages)
        {
            var multiLanguageDetailList = multiLanguages.Select(x => new ContractDefinitionLanguageDetail
            {
                ContractDefinitionId = _ContractDefinition.Id,
                MultiLanguageId = x.Id
            }).ToList();
            return multiLanguageDetailList;
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

        private List<ContractDocumentGroupDetail> MapContractDocumentGroupDetail()
        {
            if (_ContractDefinitionDataModel.documentGroupList.Any(x => x.groupName.id != "" && x.groupName != null))
            {
                var contractDocumentGroupDetails = _ContractDefinitionDataModel.documentGroupList?.Select(x => new ContractDocumentGroupDetail
                {
                    ContractDefinitionId = _ContractDefinition.Id,
                    DocumentGroupId = ZeebeMessageHelper.StringToGuid(x.groupName.id),
                    AtLeastRequiredDocument = x.atLeastRequiredDocument,
                    Required = x.required
                }).ToList();
                return contractDocumentGroupDetails;
            }
            return null;
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

        private List<ContractDocumentDetail> MapContractDocumentDetail()
        {
            var contractDocumentDetail = _ContractDefinitionDataModel.documentsList.Select(x => new ContractDocumentDetail
            {
                ContractDefinitionId = _ContractDefinition.Id,
                DocumentDefinitionId = _dbContext.DocumentDefinition.Where(y => y.Semver == x.minVersiyon && y.Code == x.name.code).Select(y => y.Id).FirstOrDefault(),
                UseExisting = x.useExisting,
                Required = x.required
            }).ToList();
            return contractDocumentDetail;
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

        private List<ContractTag> MapContractTag()
        {
            var contractTags = _ContractDefinitionDataModel.tags.Select(
                           x => new ContractTag
                           {
                               ContractDefinitionId = _ContractDefinition.Id,
                               TagId = ZeebeMessageHelper.StringToGuid(x)
                           }
                       ).ToList();
            return contractTags;
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
                epdb.EEntityPropertyType = (ushort)EEntityPropertyType.str;
                var dep = _dbContext.ContractEntityProperty.FirstOrDefault(x => x.EntityProperty.Code == i.Code);
                if (dep == null)
                {
                    _ContractDefinition.ContractEntityProperty.Add(new ContractEntityProperty
                    {
                        ContractDefinitionId = _ContractDefinition.Id,
                        EntityProperty = epdb,
                    });
                }
            }
        }
        
        private List<EntityPropertyValue> MapEntityPropertyValue()
        {
            if(_ContractDefinitionDataModel.EntityProperty != null)
            {
                return _ContractDefinitionDataModel.EntityProperty
                .Select(entityProperty => 
                    new EntityPropertyValue
                    {
                        Data = entityProperty.value
                    }
                ).ToList();
            }
            return null;
        }
        private List<core.Entity.EAV.EntityProperty> MapEntityProperty(List<EntityPropertyValue> entityPropertyValues)
        {
            if(_ContractDefinitionDataModel.EntityProperty != null)
            {
                return _ContractDefinitionDataModel.EntityProperty
                .Select(entityProperty => 
                {
                    var property = new core.Entity.EAV.EntityProperty
                    {
                        EEntityPropertyType = (ushort)EEntityPropertyType.str,
                        //EntityPropertyValueId = EntityPropertyValueId,
                        Code = entityProperty.PropertyName
                    };
                    return property;
                }).ToList();
            }
            return null;
        }

        // private void SetContractEntityProperty()
        // {
        //     if (_ContractDefinitionDataModel.EntityProperty == null)
        //     {
        //         return;
        //     }
        //     foreach (var propertyModel in _ContractDefinitionDataModel.EntityProperty)
        //     {
        //         var entityProperty = new amorphie.contract.core.Entity.EAV.EntityProperty
        //         {
        //             EEntityPropertyType = (ushort)EEntityPropertyType.str,
        //             EntityPropertyValue = new core.Entity.EAV.EntityPropertyValue { Data = propertyModel.value },
        //             Code = propertyModel.PropertyName
        //         };
        //         var existingProperty = _dbContext.EntityProperty.FirstOrDefault(x => x.Code == entityProperty.Code);
        //         if (existingProperty != null)
        //         {
        //             entityProperty = existingProperty;
        //         }
        //         var existingPropertyValue = _dbContext.EntityPropertyValue.FirstOrDefault(x => x.Data == entityProperty.EntityPropertyValue.Data);
        //         if (existingPropertyValue != null)
        //         {
        //             entityProperty.EntityPropertyValue = existingPropertyValue;
        //         }
        //         entityProperty.EEntityPropertyType = (ushort)EEntityPropertyType.str;
        //         var documentEntityProperty = _dbContext.ContractEntityProperty.FirstOrDefault(x => x.EntityProperty.Code == entityProperty.Code);
        //         if (documentEntityProperty == null)
        //         {
        //             _ContractDefinition.ContractEntityProperty.Add(new ContractEntityProperty
        //             {
        //                 ContractDefinitionId = _ContractDefinition.Id,
        //                 EntityProperty = entityProperty,
        //             });
        //         }
        //     }
        // }

        private ValidationDecision MapValidationDecision(ValidationList validationList)
        {
            if(validationList.type == EValidationType.DecisionTable)
            {
                return new ValidationDecision
                {
                     Code = validationList.decisionTable
                };
            }
            return null;
        }

        private Validation MapContractValidation(ValidationList validationList, ValidationDecision validationDecision)
        {
            if(validationList.type == EValidationType.DecisionTable)
            {
                return new Validation
                {
                    EValidationType = EValidationType.DecisionTable,
                    ValidationDecisionId = validationDecision.Id
                };
            }
            else
            {
                return new Validation
                {
                    EValidationType = EValidationType.AllValid
                };
            }
            
        }

        private ContractValidation MapContractValidation(Validation validation)
        {
            return new ContractValidation
            {
                ContractDefinitionId = _ContractDefinition.Id,
                Validations = validation
            };
        }
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

        public List<T> UpdateRelationships<T>(ICollection<T> existingItems, ICollection<T> updatedItems,
                                        Func<T, T, bool> condition) where T : class
        {
            existingItems ??= new List<T>();
            updatedItems ??= new List<T>();

            var itemsToRemove = existingItems.Where(existing => updatedItems.All(updated => !condition(existing, updated))).ToList();
            var itemsToAdd = updatedItems.Where(updated => existingItems.All(existing => !condition(existing, updated))).ToList();
            var itemsToUpdate = updatedItems
                .Except(itemsToAdd)
                .Select(updated =>
                {
                    var existingItem = existingItems.FirstOrDefault(existing => condition(existing, updated));
                    if (existingItem != null)
                    {
                        // Id'yi güncelleme
                        var propertyInfo = typeof(T).GetProperty("Id");
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(updated, propertyInfo.GetValue(existingItem));
                        }
                    }
                    return updated;
                })
                .ToList();

            if (itemsToRemove.Any())
            {
                _dbContext.RemoveRange(itemsToRemove);
            }

            if (itemsToUpdate.Any())
            {
                _dbContext.UpdateRange(itemsToUpdate);
            }

            if (itemsToAdd.Any())
            {
                _dbContext.AddRange(itemsToAdd);
            }
            _dbContext.SaveChanges();
            return itemsToAdd;
        }

        // private void UpdateNestedRelationships<TMain, TIntermediate, TDetail>(
        // ICollection<TMain> mainExistingItems, 
        // ICollection<TMain> mainUpdatedItems, 
        // DbSet<TIntermediate> intermediateDbSet,
        // DbSet<TDetail> detailDbSet,
        // Func<TMain, ICollection<TIntermediate>> mainToIntermediate,
        // Func<TIntermediate, ICollection<TDetail>> intermediateToDetail 
        // ) where TMain : class
        // where TIntermediate : class
        // where TDetail : class
        // {
        //     foreach (var mainItem in mainUpdatedItems)
        //     {
        //         var existingMainItem = mainExistingItems.FirstOrDefault(x => x.Id == mainItem.Id);
        //         if (existingMainItem != null)
        //         {
        //             // Ara tablo güncellemeleri
        //             var existingIntermediateItems = mainToIntermediate(existingMainItem);
        //             var updatedIntermediateItems = mainToIntermediate(mainItem);
        //             UpdateRelationships(existingIntermediateItems, updatedIntermediateItems, intermediateDbSet);

        //             // Alt tablo güncellemeleri
        //             foreach (var existingIntermediateItem in existingIntermediateItems)
        //             {
        //                 var existingDetailItems = intermediateToDetail(existingIntermediateItem);
        //                 var updatedDetailItems = intermediateToDetail(updatedIntermediateItems.FirstOrDefault(x => x.Id == existingIntermediateItem.Id));
        //                 UpdateRelationships(existingDetailItems, updatedDetailItems, detailDbSet);
        //             }
        //         }
        //     }
        // }

        public async Task<ContractDefinition> DataModelToContractDefinitionUpdate(dynamic contractDefinitionDataUpdateDynamic, Guid id)
        {
            _ContractDefinitionDataModelDynamic = contractDefinitionDataUpdateDynamic;
            try
            {
                DynamicToContractDefinitionDataModel();
                SetContractDefinitionDefault(id);
                var contractDefinition = _dbContext.ContractDefinition.FirstOrDefault(x=>x.Id == _ContractDefinition.Id);
                if (contractDefinition==null)
                {
                    throw new Exception("Güncellemek istediğiniz döküman bulunmamakta.");
                }
                
                // Mevcut veriyi history e geçirme işlemi yapılacak
                // Eski veriyi yenisiyle güncellemesi işlemi yapılacak

                // ContractDefinition tablosundaki alanları güncelle
                contractDefinition.ModifiedAt = DateTime.UtcNow;
                contractDefinition.BankEntity = _ContractDefinition.BankEntity;

                //ENTITY PROPERTY UPDATE
                var oldEntityPropertyValueContract = contractDefinition.ContractEntityProperty.Select(x=>x.EntityProperty.EntityPropertyValue).ToList();
                var newEntityPropertValueContract = MapEntityPropertyValue();
                Func<EntityPropertyValue,EntityPropertyValue,bool> conditionEntityPropertyValue = (exist,update) => exist.Data == update.Data;
                var entityPropertyValue = UpdateRelationships(oldEntityPropertyValueContract,newEntityPropertValueContract,conditionEntityPropertyValue);
                if(entityPropertyValue.Any())
                {
                    var oldEntityPropertyContract = contractDefinition.ContractEntityProperty.Select(x=>x.EntityProperty).ToList();
                    var newEntityPropertyContract = MapEntityProperty(entityPropertyValue);
                    Func<core.Entity.EAV.EntityProperty,core.Entity.EAV.EntityProperty,bool> conditionEntityProperty = (exist,update) => exist.Code == update.Code;
                    UpdateRelationships(oldEntityPropertyContract,newEntityPropertyContract,conditionEntityProperty);
                }

                // TAG UPDATE
                var oldTags = contractDefinition.ContractTags;
                var newTags = MapContractTag();
                Func<ContractTag, ContractTag, bool> conditionTag = (exist,update) => exist.TagId == update.TagId;
                UpdateRelationships(oldTags,newTags,conditionTag);
                // VALIDATION UPDATE

                // DOCUMENT LIST UPDATE
                var oldDocs = contractDefinition.ContractDocumentDetails;
                var newDocs = MapContractDocumentDetail();
                Func<ContractDocumentDetail, ContractDocumentDetail, bool> conditionDocDetail = (exist,update) => exist.DocumentDefinitionId == update.DocumentDefinitionId;
                UpdateRelationships(oldDocs,newDocs,conditionDocDetail);

                // DOCUMENT GROUP LIST UPDATE
                var oldDocGroup = contractDefinition.ContractDocumentGroupDetails;
                var newDocGroup = MapContractDocumentGroupDetail();
                Func<ContractDocumentGroupDetail,ContractDocumentGroupDetail,bool> conditionDocGrupDetail = (exist,update) => exist.DocumentGroupId == update.DocumentGroupId;
                UpdateRelationships(oldDocGroup,newDocGroup,conditionDocGrupDetail);

                //TITLE UPDATE
                var oldMultiLang = contractDefinition.ContractDefinitionLanguageDetails.Select(x=>x.MultiLanguage).ToList();
                var newMultiLang = MapContractDefinitionLanguage();
                Func<MultiLanguage,MultiLanguage,bool> conditionMultiLanguage = (exist,update) => exist.Code == update.Code && exist.LanguageType == update.LanguageType;
                UpdateRelationships(oldMultiLang,newMultiLang,conditionMultiLanguage);

                var oldMultiLangDetail = contractDefinition.ContractDefinitionLanguageDetails;
                var newMultiLangDetail = MapContractDefinitionLanguageDetail(newMultiLang); // BİR SIKINTI OLABİLİR
                Func<ContractDefinitionLanguageDetail,ContractDefinitionLanguageDetail,bool> conditionMultiLanguageDetail = (exist,update) => exist.MultiLanguageId == update.MultiLanguageId;
                UpdateRelationships(oldMultiLangDetail,newMultiLangDetail,conditionMultiLanguageDetail);

                //VALIDATION UPDATE
                //var contractValidation = _ContractDefinitionDataModel.validationList.First();
                //var newValidationDecision = MapValidationDecision(_ContractDefinitionDataModel.validationList.First());
                
                


                await _dbContext.SaveChangesAsync();

            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }
            return _ContractDefinition;
        }
    }
}