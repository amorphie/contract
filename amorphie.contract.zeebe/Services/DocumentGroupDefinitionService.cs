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
        Task<DocumentGroup> DataModelToDocumentGroupDefinition(dynamic documentDefinitionGroupDataDynamic, Guid id);
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
            // var documentGroupDetail = _documentDefinitionDataModel.documents.Select(x => new DocumentGroupDetail
            // {
            //     DocumentDefinitionId = x.document.Id,
            //     DocumentGroupId = _documentGroup.Id,
            // });

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

        public async Task<DocumentGroup> DataModelToDocumentGroupDefinition(dynamic documentDefinitionDataModelDynamic, Guid id)
        {
            _documentDefinitionDataModelDynamic = documentDefinitionDataModelDynamic;
            try
            {
                DynamicToDocumentDefinitionDataModel();
                SetDocumentGroupDefault(id);

                SetDocumentGroupLanguageDetail();
                SetDocumentGroupDetail();
                _dbContext.DocumentGroup.Add(_documentGroup);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _documentGroup;

        }

    }
}