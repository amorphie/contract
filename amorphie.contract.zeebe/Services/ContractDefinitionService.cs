using amorphie.contract.core.Entity.Common;
using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Services.Interfaces;
using Newtonsoft.Json;
using amorphie.contract.zeebe.Model.ContractDefinitionDataModel;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Enum;

namespace amorphie.contract.zeebe.Services
{
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
            var activeStatus = _dbContext.Status.FirstOrDefault(x => x.Code == "active");
            if (activeStatus == null)
            {
                activeStatus = new Status { Code = "active" };
            }
            _ContractDefinition.Id = id;
            _ContractDefinition.StatusId = activeStatus.Id;
            _ContractDefinition.Code = _ContractDefinitionDataModel.code;

        }
        private void DynamicToContractDefinitionDataModel()
        {
            _ContractDefinitionDataModel = new ContractDefinitionDataModel();
            _ContractDefinitionDataModel = JsonConvert.DeserializeObject<ContractDefinitionDataModel>(_ContractDefinitionDataModelDynamic.ToString());
        }
        private void SetContractDocumentGroupDetail()
        {
            var multiLanguageList = _ContractDefinitionDataModel.documentGroupList.Select(x => new ContractDocumentGroupDetail
            {
                ContractDefinitionId = _ContractDefinition.Id,
                DocumentGroupId = ZeebeMessageHelper.StringToGuid(x.groupName),
                AtLeastRequiredDocument = (uint)x.atLeastRequiredDocument,
                Required = x.required
            });
        }
        private void SetContractDocumentDetail()
        {
            var contractDocumentDetail = _ContractDefinitionDataModel.documentsList.Select(x => new ContractDocumentDetail
            {
                ContractDefinitionId = _ContractDefinition.Id,
                DocumentDefinitionId = ZeebeMessageHelper.StringToGuid(x.name),
                UseExisting = (ushort)Enum.Parse(typeof(EUseExisting), x.useExisting),
                Semver = x.minVersiyon,
                Required = x.required
            });
            _ContractDefinition.ContractDocumentDetails = contractDocumentDetail.ToList();

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

            var ep = _ContractDefinitionDataModel.EntityPropertyList.Select(x => new amorphie.contract.core.Entity.EAV.EntityProperty
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
                epdb.EEntityPropertyType = (ushort)EEntityPropertyType.str;
                var dep = _dbContext.DocumentEntityProperty.FirstOrDefault(x => x.EntityProperty.Code == i.Code);
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
        private void SetContractValidation()
        {
            var list = _ContractDefinitionDataModel.validationList.Select(
                           x => new ContractValidation
                           {
                               ContractDefinitionId = _ContractDefinition.Id,
                               Validations = new Validation
                               {
                                   EValidationType = (EValidationType)Enum.Parse(typeof(EValidationType), x.type),
                               },
                           }
                       ).ToList();
            _ContractDefinition.ContractValidations = list;
        }
        public async Task<ContractDefinition> DataModelToContractDefinition(dynamic documentDefinitionDataModelDynamic, Guid id)
        {
            _ContractDefinitionDataModelDynamic = documentDefinitionDataModelDynamic;
            try
            {
                DynamicToContractDefinitionDataModel();
                SetContractDefinitionDefault(id);
                SetContractDocumentGroupDetail();
                SetContractDocumentDetail();
                SetContractTag();
                SetContractEntityProperty();
                SetContractValidation();
                _dbContext.ContractDefinition.Add(_ContractDefinition);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ContractDefinition;

        }

    }
}