using System.Xml.Linq;
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
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.zeebe.Model.DocumentGroupDataModel;

namespace amorphie.contract.zeebe.Services
{
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
            var documentGroupDetail = _documentDefinitionDataModel.documents.Select(x => new DocumentGroupDetail
            {
                DocumentDefinitionCode = x.document.name,
                DocumentGroupId = _documentGroup.Id,
                MinVersion = x.minVersiyon
            });
            _documentGroup.DocumentGroupDetails = documentGroupDetail.ToList();

        }
        private void SetDocumentGroupDefault(Guid id)
        {
            _documentGroup = new DocumentGroup();
            var activeStatus = _dbContext.Status.FirstOrDefault(x => x.Code == "active");
            if (activeStatus == null)
            {
                activeStatus = new Status { Code = "active" };
            }
            _documentGroup.Id = id;
            _documentGroup.StatusId = activeStatus.Id;
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