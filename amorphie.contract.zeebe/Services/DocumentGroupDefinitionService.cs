using System.Xml.Linq;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Model.DocumentDefinitionDataModel;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.zeebe.Model.DocumentGroupDataModel;

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
            var multiLanguageList = _documentDefinitionDataModel.titles.Select(x => new MultiLanguage
            {
                Name = x.title,
                LanguageTypeId = ZeebeMessageHelper.StringToGuid(x.language),
                Code = _documentGroup.Code
            });
            _documentGroup.DocumentGroupLanguageDetail = multiLanguageList.Select(x => new DocumentGroupLanguageDetail
            {
                DocumentGroupId = _documentGroup.Id,
                MultiLanguage = x
            }).ToList();

        }

        private void SetDocumentGroupDetail()
        {
            var asd = _dbContext.DocumentDefinition.ToList();
            var tt = _dbContext.DocumentDefinition.Where(y => y.Semver == "1.0.0" && y.Code == "doc-kvkk-test").Select(y => y.Id).FirstOrDefault();

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

        public async Task<DocumentGroup> UpdateGroup(dynamic documentDefinitionDataModelDynamic, Guid id)
        {
            try
            {
                DataModelToDocumentGroupDefinition(documentDefinitionDataModelDynamic, id);
                var documentGroup = _dbContext.DocumentGroup.AsSplitQuery().FirstOrDefault(x => x.Id == id);

                documentGroup.Code = _documentGroup.Code;

                foreach (var detailObject in _documentGroup.DocumentGroupLanguageDetail)
                {
                    var multiLangObject = detailObject.MultiLanguage;
                    var entity = documentGroup.DocumentGroupLanguageDetail.Where(x => x.MultiLanguage.LanguageTypeId == multiLangObject.LanguageTypeId).FirstOrDefault();

                    if (entity is not null)
                    {
                        entity.MultiLanguage.Name = multiLangObject.Name;
                        entity.MultiLanguage.Code = multiLangObject.Code;
                    }
                    else
                    {
                        _dbContext.Entry(detailObject).State = EntityState.Added;
                        _dbContext.Entry(multiLangObject).State = EntityState.Added;
                        documentGroup.DocumentGroupLanguageDetail.Add(detailObject);
                    }
                }

                foreach (var detailObject in _documentGroup.DocumentGroupDetails)
                {
                    var entity = documentGroup.DocumentGroupDetails.Where(x => x.DocumentDefinitionId == detailObject.DocumentDefinitionId).FirstOrDefault();

                    if (entity is null)
                    {
                        _dbContext.Entry(detailObject).State = EntityState.Added;
                        documentGroup.DocumentGroupDetails.Add(detailObject);
                    }
                }

                var removeLanguages = documentGroup.DocumentGroupLanguageDetail.Where(x => !_documentGroup.DocumentGroupLanguageDetail.Select(z => z.MultiLanguage.LanguageTypeId).ToList().Contains(x.MultiLanguage.LanguageTypeId)).ToList();
                documentGroup.DocumentGroupLanguageDetail = documentGroup.DocumentGroupLanguageDetail.Except(removeLanguages).ToList();

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