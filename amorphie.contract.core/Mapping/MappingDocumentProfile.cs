using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using AutoMapper;

namespace amorphie.contract.core.Mapping
{
    public class MappingDocumentProfile : Profile
    {
        public MappingDocumentProfile()
        {
            CreateMap<Document, Document>().ReverseMap();
            CreateMap<DocumentAllowed, DocumentAllowed>().ReverseMap();
            CreateMap<DocumentAllowedDetail, DocumentAllowedDetail>().ReverseMap();
            CreateMap<DocumentAllowedType, DocumentAllowedType>().ReverseMap();
            CreateMap<DocumentContent, DocumentContent>().ReverseMap();
            CreateMap<DocumentDefinition, DocumentDefinition>().ReverseMap();
            CreateMap<DocumentDefinitionLanguageDetail, DocumentDefinitionLanguageDetail>().ReverseMap();
            CreateMap<DocumentEntityProperty, DocumentEntityProperty>().ReverseMap();
            CreateMap<DocumentFormat, DocumentFormat>().ReverseMap();
            CreateMap<DocumentFormatDetail, DocumentFormatDetail>().ReverseMap();
            CreateMap<DocumentFormatType, DocumentFormatType>().ReverseMap();

            CreateMap<DocumentFormIO, DocumentFormIO>().ReverseMap();
            CreateMap<DocumentFormIODetail, DocumentFormIODetail>().ReverseMap();


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





        }
    }
}