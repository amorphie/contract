using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.data.Contexts;
using amorphie.contract.zeebe.Model.DocumentDefinitionDataModel;
using amorphie.contract.zeebe.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace amorphie.contract.zeebe.Services
{
    public class DocumentDefinitionService : IDocumentDefinitionService
    {
        ProjectDbContext _dbContext;
        DocumentDefinitionDataModel _documentDefinitionDataModel;
        DocumentDefinition documentdef;

        public DocumentDefinitionService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DocumentDefinition> DataModelToDocumentDefinition(DocumentDefinitionDataModel documentDefinitionDataModel)
        {

            documentdef = new core.Entity.Document.DocumentDefinition
            {
                Code = documentDefinitionDataModel.data.name,
                StatusId = ZeebeMessageHelper.StringToGuid(documentDefinitionDataModel.data.status),
                BaseStatusId = ZeebeMessageHelper.StringToGuid(documentDefinitionDataModel.data.basestatus),
            };
            documentdef.DocumentDefinitionLanguageDetails = documentDefinitionDataModel.data.titles.Select(x => new core.Entity.Document.DocumentDefinitionLanguageDetail
            {
                DocumentDefinitionId = documentdef.Id,

                MultiLanguage = new MultiLanguage
                {
                    Name = x.title,
                    LanguageTypeId = ZeebeMessageHelper.StringToGuid(x.language),
                    Code = documentdef.Code
                }
            }).ToList();
          
            documentdef.DocumentTagsDetails = documentDefinitionDataModel.data.tags.Select(
                x => new DocumentTagsDetail
                {
                    DocumentDefinitionId = documentdef.Id,
                    Tags = new core.Entity.Common.Tag
                    {
                        Contact = x.Contact,
                        Code = x.tag
                    }
                }
            ).ToList();
            var manuelControl = documentDefinitionDataModel.data.documentManuelControl;
            documentdef.DocumentOperations = documentDefinitionDataModel.data.TagsOperation.Select(x => new DocumentOperations
            {
                DocumentDefinitionId = documentdef.Id,
                DocumentManuelControl = manuelControl,
                Tags = new core.Entity.Common.Tag
                {
                    Contact = x.Contact,
                    Code = x.tag
                }
            }).First();
            documentdef.DocumentOptimize = documentDefinitionDataModel.data.optimize.Select(x => new DocumentOptimize
            {

                DocumentOptimizeType = new DocumentOptimizeType
                {
                    Code = x.transformto
                },
                Size = x.size,
                DocumentDefinitionId = documentdef.Id
            }).First();
            var allowedClientDetail = documentDefinitionDataModel.data.allowedclients.Select(x => new DocumentAllowedClientDetail
            {
                DocumentDefinitionId = documentdef.Id,
                DocumentAllowedClientId = ZeebeMessageHelper.StringToGuid(x.select)
                // DocumentAllowedClients = new DocumentAllowedClient{
                //     Code =x.select
                // }
            }).ToList();
            var documentFormatDetail = documentDefinitionDataModel.data.allowedformats.Select(x => new DocumentFormatDetail
            {
                DocumentDefinitionId = documentdef.Id,
                DocumentFormat = new DocumentFormat
                {
                    DocumentFormatTypeId = ZeebeMessageHelper.StringToGuid(x.format),
                    DocumentSizeId = ZeebeMessageHelper.StringToGuid(x.maxsizekilobytes)
                }
            }).ToList();
            documentdef.DocumentUpload = new core.Entity.Document.DocumentTypes.DocumentUpload
            {
                Required = documentDefinitionDataModel.data.scarequired,
                DocumentFormatDetails = documentFormatDetail,
                DocumentAllowedClientDetails = allowedClientDetail
            };
            _dbContext.DocumentDefinition.Add(documentdef);
            _dbContext.SaveChanges();
            return new DocumentDefinition();
        }

    }
}