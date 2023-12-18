using System.Xml.Serialization;
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
                             MultilanguageText = x.DocumentDefinition.DocumentDefinitionLanguageDetails
                                .Select(a => new MultilanguageText
                                {
                                    Label = a.MultiLanguage.Name,
                                    Language = a.MultiLanguage.LanguageType.Code
                                }).ToList(),
                             DocumentOperations = new DocumentOperationsModel
                             {
                                 DocumentManuelControl = x.DocumentDefinition.DocumentOperations.DocumentManuelControl,
                                 DocumentOperationsTagsDetail = x.DocumentDefinition.DocumentOperations.DocumentOperationsTagsDetail.Select(x => new TagModel
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
                .ConvertUsing((source, destination, context) => ConvertDocumentDefinitionToViewModel(source));


            CreateMap<DocumentGroup, DocumentGroupViewModel>()
            .ConvertUsing(x => new DocumentGroupViewModel
            {
                Id = x.Id,
                Code = x.Code,
                Status = x.Status != null ? x.Status.Code : null,
                MultilanguageText = x.DocumentGroupLanguageDetail != null ? x.DocumentGroupLanguageDetail
                                    .Select(a => new MultilanguageText
                                    {
                                        Label = a.MultiLanguage.Name,
                                        Language = a.MultiLanguage.LanguageType.Code
                                    }).ToList() : null,
                // DocumentDefinitionList = x.DocumentGroupDetails.
                //                     Select(x => ObjectMapper.Mapper.Map<DocumentDefinitionViewModel>(x.DocumentDefinition)).ToList()

            });

        }
        private DocumentDefinitionViewModel ConvertDocumentDefinitionToViewModel(DocumentDefinition source, string language)
        {
            var viewModel = new DocumentDefinitionViewModel
            {
                // Diğer özellikler
            };

            // Belirtilen dilin etiketini getir
            var languageDetail = source.DocumentDefinitionLanguageDetails?
                .FirstOrDefault(a => a.MultiLanguage != null && a.MultiLanguage.LanguageType.Code == language);

            if (languageDetail != null)
            {
                // Belirtilen dil bulunduysa, etiketi "Name" özelliğine eşitle
                viewModel.Name = languageDetail.MultiLanguage?.Name;
            }
            else
            {
                // Belirtilen dil bulunamazsa, ilk dilin etiketini "Name" özelliğine eşitle
                viewModel.Name = source.DocumentDefinitionLanguageDetails?
                    .FirstOrDefault(a => a.MultiLanguage != null)?.MultiLanguage?.Name;
            }

            // Diğer özellikler

            return viewModel;
        }
        private DocumentDefinitionViewModel ConvertDocumentDefinitionToViewModel(DocumentDefinition source)
        {
            var viewModel = new DocumentDefinitionViewModel
            {
                Id = source.Id,
                Code = source.Code,
                Status = source.Status.Code,
                Semver = source.Semver,
                BaseStatus = source.BaseStatus.Code,
                MultilanguageText = source.DocumentDefinitionLanguageDetails?
                    .Select(a => new MultilanguageText
                    {
                        Label = a.MultiLanguage != null ? a.MultiLanguage.Name : null,
                        Language = a.MultiLanguage?.LanguageType?.Code
                    }).ToList(),
                EntityProperties = source.DocumentEntityPropertys?.Select(a => new EntityPropertyView
                {
                    Code = a.EntityProperty?.Code,
                    EntityPropertyValue = a.EntityProperty?.EntityPropertyValue?.Data
                }).ToList(),
                Tags = source.DocumentTagsDetails?.Select(a => new TagsView
                {
                    Code = a.Tags?.Code,
                    Contact = a.Tags?.Contact,
                }).ToList(),
                DocumentUpload = source.DocumentUpload != null ? new DocumentUploadView
                {
                    Required = source.DocumentUpload.Required,
                    DocumentAllowedClientDetail = source.DocumentUpload.DocumentAllowedClientDetails?
                        .Select(a => a.DocumentAllowedClients?.Code).ToList(),
                    DocumentFormatDetail = source.DocumentUpload.DocumentFormatDetails?
                        .Select(a => new DocumentFormatDetailView
                        {
                            Size = a.DocumentFormat?.DocumentSize?.KiloBytes.ToString(),
                            FormatType = a.DocumentFormat?.DocumentFormatType?.Code,
                            FormatContentType = a.DocumentFormat?.DocumentFormatType?.ContentType
                        }).ToList()
                } : null,
                DocumentOnlineSing = source.DocumentOnlineSing != null ? new DocumentOnlineSingView
                {
                    DocumentAllowedClientDetail = source.DocumentOnlineSing.DocumentAllowedClientDetails?
                        .Select(a => a.DocumentAllowedClients?.Code).ToList(),
                    DocumentTemplateDetails = source.DocumentOnlineSing.DocumentTemplateDetails?
                        .Select(a => new DocumentTemplateDetailsView
                        {
                            Code = a.DocumentTemplate?.Code,
                            LanguageType = a.DocumentTemplate?.LanguageType?.Code,
                        }).ToList()
                } : null,
                DocumentOptimize = source.DocumentOptimize != null ? new DocumentOptimizeView
                {
                    Size = source.DocumentOptimize.Size,
                    Code = source.DocumentOptimize?.DocumentOptimizeType?.Code
                } : null,
                DocumentOperations = source.DocumentOperations != null ? new DocumentOperationsView
                {
                    DocumentManuelControl = source.DocumentOperations.DocumentManuelControl,
                    DocumentOperationsTagsDetail = source.DocumentOperations.DocumentOperationsTagsDetail?
                        .Select(a => new TagsView
                        {
                            Code = a.Tags?.Code,
                            Contact = a.Tags?.Contact,
                        }).ToList()
                } : null
            };

            return viewModel;
        }
    }
}