using System.Xml.XPath;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.core.Model.Document;
using AutoMapper;
using amorphie.core.Base;

namespace amorphie.contract.core.Mapping
{
    public class MappingDocumentProfile : Profile
    {
        public MappingDocumentProfile()
        {
            CreateMap<Document, Document>().ReverseMap();
            CreateMap<DocumentAllowedClient, DocumentAllowedClient>().ReverseMap();
            CreateMap<DocumentAllowedClientDetail, DocumentAllowedClientDetail>().ReverseMap();
            CreateMap<DocumentContent, DocumentContent>().ReverseMap();
            CreateMap<DocumentDefinition, DocumentDefinition>().ReverseMap();
            CreateMap<DocumentDefinitionLanguageDetail, DocumentDefinitionLanguageDetail>().ReverseMap();
            CreateMap<DocumentEntityProperty, DocumentEntityProperty>().ReverseMap();
            CreateMap<DocumentFormat, DocumentFormat>().ReverseMap();
            CreateMap<DocumentFormatDetail, DocumentFormatDetail>().ReverseMap();
            CreateMap<DocumentFormatType, DocumentFormatType>().ReverseMap();




            CreateMap<DocumentOperations, DocumentOperations>().ReverseMap();
            CreateMap<DocumentOptimize, DocumentOptimize>().ReverseMap();
            CreateMap<DocumentOptimizeType, DocumentOptimizeType>().ReverseMap();
            CreateMap<DocumentSize, DocumentSize>().ReverseMap();
            CreateMap<DocumentTagsDetail, DocumentTagsDetail>().ReverseMap();
            CreateMap<DocumentTemplate, DocumentTemplate>().ReverseMap();
            CreateMap<DocumentTemplateDetail, DocumentTemplateDetail>().ReverseMap();
            // CreateMap<DocumentVersions, DocumentVersions>().ReverseMap();
            #region DocumentGroup
            CreateMap<DocumentGroup, DocumentGroup>().ReverseMap();
            CreateMap<DocumentGroupDetail, DocumentGroupDetail>().ReverseMap();
            CreateMap<DocumentGroupLanguageDetail, DocumentGroupLanguageDetail>().ReverseMap();
            #endregion
            #region documentType
            CreateMap<DocumentOnlineSing, DocumentOnlineSing>().ReverseMap();
            CreateMap<DocumentRender, DocumentRender>().ReverseMap();
            CreateMap<DocumentUpload, DocumentUpload>().ReverseMap();
            #endregion

            CreateMap<Document, RootDocumentModel>()
                     .ConstructUsing(x => new RootDocumentModel
                     {
                         DocumentDefinitionId = x.DocumentDefinitionId.ToString(),
                         StatuCode = x.Status.Code,
                         CreatedAt = x.CreatedAt,
                         DocumentDefinition = new DocumentDefinitionModel
                         {
                             Code = x.DocumentDefinition.Code,
                             MultilanguageText = x.DocumentDefinition.DocumentDefinitionLanguageDetails!
                                .Select(a => new MultilanguageText
                                {
                                    Label = a.MultiLanguage.Name,
                                    Language = a.MultiLanguage.LanguageType.Code
                                }).ToList(),
                             DocumentOperations = new DocumentOperationsModel
                             {
                                 DocumentManuelControl = x.DocumentDefinition.DocumentOperations!.DocumentManuelControl,
                                 DocumentOperationsTagsDetail = x.DocumentDefinition.DocumentOperations.DocumentOperationsTagsDetail!.Select(x => new TagModel
                                 {
                                     Contact = x.Tags.Contact,
                                     Code = x.Tags.Code
                                 }).ToList()
                             }
                         },
                         DocumentContent = new DocumentContentModel
                         {
                             ContentData = x.DocumentContent.ContentData,
                             KiloBytesSize = x.DocumentContent.KiloBytesSize,
                             ContentType = x.DocumentContent.ContentType,
                             ContentTransferEncoding = x.DocumentContent.ContentTransferEncoding,
                             Name = x.DocumentContent.Name,
                             Id = x.DocumentContent.Id.ToString()

                         }
                     });

        }
    }
}