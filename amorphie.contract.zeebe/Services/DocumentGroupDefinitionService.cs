using amorphie.contract.core.Entity.Common;
using amorphie.contract.infrastructure.Contexts;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.zeebe.Model.DocumentGroupDataModel;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.application;
using amorphie.contract.core.Model.History;

namespace amorphie.contract.zeebe.Services
{
    public interface IDocumentGroupDefinitionService
    {
        Task<DocumentGroup> CreateGroup(dynamic documentDefinitionGroupDataDynamic, Guid id);
        Task<DocumentGroup> UpdateGroup(dynamic documentDefinitionGroupDataDynamic, Guid id);
    }
    public class DocumentGroupDefinitionService : IDocumentGroupDefinitionService
    {
        ProjectDbContext _dbContext;
        DocumentGroupDataModel _documentDefinitionDataModel;
        DocumentGroup _documentGroup;
        dynamic? _documentDefinitionDataModelDynamic;

        public DocumentGroupDefinitionService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private void DynamicToDocumentDefinitionDataModel()
        {
            _documentDefinitionDataModel = new DocumentGroupDataModel();
            _documentDefinitionDataModel = JsonConvert.DeserializeObject<DocumentGroupDataModel>(_documentDefinitionDataModelDynamic.ToString());
        }

        private void SetDocumentGroupLanguageDetail()
        {
            // var multiLanguageList = _documentDefinitionDataModel.titles.Select(x => new MultiLanguage
            // {
            //     Name = x.title,
            //     LanguageTypeId = ZeebeMessageHelper.StringToGuid(x.language),
            //     Code = _documentGroup.Code
            // });
            // _documentGroup.DocumentGroupLanguageDetail = multiLanguageList.Select(x => new DocumentGroupLanguageDetail
            // {
            //     DocumentGroupId = _documentGroup.Id,
            //     MultiLanguage = x
            // }).ToList();

            //TODO [LANG] yukarıdaki kod refactor edilmeli.
            var langTypes = _dbContext.LanguageType.ToDictionary(i => i.Id, i => i.Code);
            _documentGroup.Titles = _documentDefinitionDataModel.titles.ToDictionary(item => langTypes[ZeebeMessageHelper.StringToGuid(item.language)], item => item.title);

        }

        private void SetDocumentGroupDetail()
        {
            var documentGroupDetail = _documentDefinitionDataModel.documents.Select(x => new DocumentGroupDetail
            {
                DocumentDefinitionId = _dbContext.DocumentDefinition.Where(y => y.Semver == x.minVersiyon && y.Code == x.document.code).Select(y => y.Id).FirstOrDefault(),
                DocumentGroupId = _documentGroup.Id,
            });

            _documentGroup.DocumentGroupDetails = documentGroupDetail.ToList();
        }

        private void SetDocumentGroupDefault(Guid id)
        {
            _documentGroup = new DocumentGroup();

            _documentGroup.Id = id;
            _documentGroup.Status = core.Enum.EStatus.Active;
            _documentGroup.Code = _documentDefinitionDataModel.code;

        }

        private void DataModelToDocumentGroupDefinition(dynamic documentDefinitionDataModelDynamic, Guid id)
        {
            _documentDefinitionDataModelDynamic = documentDefinitionDataModelDynamic;
            try
            {
                DynamicToDocumentDefinitionDataModel();
                SetDocumentGroupDefault(id);
                SetDocumentGroupLanguageDetail();
                SetDocumentGroupDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DocumentGroup> CreateGroup(dynamic documentDefinitionDataModelDynamic, Guid id)
        {
            DataModelToDocumentGroupDefinition(documentDefinitionDataModelDynamic, id);
            _dbContext.DocumentGroup.Add(_documentGroup);
            _dbContext.SaveChanges();

            return _documentGroup;
        }

        private void SetContractDefinitionHistory(DocumentGroup existingDocumentGroup)
        {
            var documentGroupHistoryModel = ObjectMapperApp.Mapper.Map<DocumentGroupHistoryModel>(existingDocumentGroup);
            var documentGroupHistory = new DocumentGroupHistory
            {
                DocumentGroupHistoryModel = documentGroupHistoryModel,
                DocumentGroupId = _documentGroup.Id
            };
            _dbContext.DocumentGroupHistory.Add(documentGroupHistory);
        }

        public async Task<DocumentGroup> UpdateGroup(dynamic documentDefinitionDataModelDynamic, Guid id)
        {
            try
            {
                DataModelToDocumentGroupDefinition(documentDefinitionDataModelDynamic, id);
                var documentGroup = _dbContext.DocumentGroup.AsSplitQuery().FirstOrDefault(x => x.Id == id);
                SetContractDefinitionHistory(documentGroup);
                documentGroup.Code = _documentGroup.Code;

                foreach (var detailObject in _documentGroup.DocumentGroupDetails)
                {
                    var entity = documentGroup.DocumentGroupDetails.Where(x => x.DocumentDefinitionId == detailObject.DocumentDefinitionId).FirstOrDefault();

                    if (entity is null)
                    {
                        _dbContext.Entry(detailObject).State = EntityState.Added;
                        documentGroup.DocumentGroupDetails.Add(detailObject);
                    }
                }

                var removeDetails = documentGroup.DocumentGroupDetails.Where(x => !_documentGroup.DocumentGroupDetails.Select(z => z.DocumentDefinitionId).ToList().Contains(x.DocumentDefinitionId)).ToList();
                documentGroup.DocumentGroupDetails = documentGroup.DocumentGroupDetails.Except(removeDetails).ToList();

                _dbContext.SaveChanges();

                return _documentGroup;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}