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
            CreateMap<DocumentDefinition, DocumentDefinitionViewModel>()
                    .ConvertUsing(x => new DocumentDefinitionViewModel
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Status = x.Status.Code,
                        BaseStatus = x.BaseStatus.Code,
                        MultilanguageText = x.DocumentDefinitionLanguageDetails!
                                    .Select(a => new MultilanguageText
                                    {
                                        Label = a.MultiLanguage!.Name,
                                        Language = a.MultiLanguage!.LanguageType!.Code
                                    }).ToList(),
                        EntityProperties = x.DocumentEntityPropertys!.Select(a => new EntityPropertyView
                        {
                            Code = a.EntityProperty!.Code,
                            EntityPropertyValue = a.EntityProperty!.EntityPropertyValue!.Data
                        }).ToList(),
                        Tags = x.DocumentTagsDetails!.Select(a => new TagsView
                        {
                            Code = a.Tags!.Code,
                            Contact = a.Tags!.Contact,
                        }).ToList(),
                        DocumentUpload = x.DocumentUpload != null ? new DocumentUploadView
                        {
                            Required = x.DocumentUpload!.Required,
                            DocumentAllowedClientDetail = x.DocumentUpload!.DocumentAllowedClientDetails.
                    Select(a => a.DocumentAllowedClients!.Code).ToList(),
                            DocumentFormatDetail = x.DocumentUpload!.DocumentFormatDetails.
                    Select(a => new DocumentFormatDetailView
                    {
                        Size = a.DocumentFormat!.DocumentSize!.KiloBytes.ToString(),
                        FormatType = a.DocumentFormat!.DocumentFormatType!.Code,
                        FormatContentType = a.DocumentFormat!.DocumentFormatType!.ContentType
                    }).ToList()
                        } : null,
                        DocumentOnlineSing = x.DocumentOnlineSing != null ? new DocumentOnlineSingView
                        {
                            Semver = x.DocumentOnlineSing!.Semver,
                            DocumentAllowedClientDetail = x.DocumentOnlineSing!.DocumentAllowedClientDetails.
                    Select(a => a.DocumentAllowedClients!.Code).ToList(),
                            DocumentTemplateDetails = x.DocumentOnlineSing.DocumentTemplateDetails
                    .Select(a => new DocumentTemplateDetailsView
                    {
                        Code = a.DocumentTemplate!.Code,
                        LanguageType = a.DocumentTemplate!.LanguageType.Code,
                    }).ToList()
                        } : null,
                        DocumentOptimize = x.DocumentOptimize != null ? new DocumentOptimizeView
                        {
                            Size = x.DocumentOptimize!.Size,
                            Code = x.DocumentOptimize!.DocumentOptimizeType!.Code
                        } : null,
                        DocumentOperations = x.DocumentOperations != null ? new DocumentOperationsView
                        {
                            DocumentManuelControl = x.DocumentOperations!.DocumentManuelControl,
                            DocumentOperationsTagsDetail = x.DocumentOperations!.DocumentOperationsTagsDetail!.Select(a => new TagsView
                            {
                                Code = a.Tags!.Code,
                                Contact = a.Tags!.Contact,
                            }).ToList()
                        } : null
                    });

        }
    }
}