using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.core.Entity.EAV;
using amorphie.core.Base;
using AutoMapper;

namespace amorphie.contract.application
{
    public class DocumentMapProfile : Profile
    {
        public DocumentMapProfile()
        {
            CreateMap<DocumentContentDto, DocumentContent>().ReverseMap();

            CreateMap<DocumentOperationsDto, DocumentOperations>().ReverseMap();

            CreateMap<DocumentOperationsTagsDetail, DocumentOperations>().ReverseMap();

            CreateMap<DocumentDefinitionLanguageDetail, MultilanguageText>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.MultiLanguage.Name))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.MultiLanguage.LanguageType.Code)).ReverseMap();

            CreateMap<DocumentGroupLanguageDetail, MultilanguageText>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.MultiLanguage.Name))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.MultiLanguage.LanguageType.Code)).ReverseMap();


            CreateMap<DocumentInstanceInputDto, DocumentContent>()
                .ForMember(dest => dest.ContentData, opt => opt.MapFrom(src => src.FileContext.ToString()))
                .ForMember(dest => dest.KiloBytesSize, opt => opt.MapFrom(src => src.FileContext.ToString().Length.ToString()))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.FileType))
                .ForMember(dest => dest.MinioObjectName, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.ContentTransferEncoding, opt => opt.MapFrom(src => src.FileType))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString())).ReverseMap();

            CreateMap<TagDto, DocumentTagsDetail>()
                    .ForPath(dest => dest.Tags.Code, opt => opt.MapFrom(src => src.Code))
                    .ForPath(dest => dest.Tags.Contact, opt => opt.MapFrom(src => src.Contact)).ReverseMap();

            CreateMap<DocumentOptimizeDto, DocumentOptimize>()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForPath(dest => dest.DocumentOptimizeType.Code, opt => opt.MapFrom(src => src.Code)).ReverseMap();

            CreateMap<DocumentTemplateDetailsDto, DocumentTemplateDetail>()
                .ForPath(dest => dest.DocumentTemplate.Code, opt => opt.MapFrom(src => src.Code))
                .ForPath(dest => dest.DocumentTemplate.LanguageType.Code, opt => opt.MapFrom(src => src.LanguageType))
                .ForPath(dest => dest.DocumentTemplate.Version, opt => opt.MapFrom(src => src.Version)).ReverseMap();

            CreateMap<DocumentOnlineSing, DocumentOnlineSingDto>()
                .ForMember(dest => dest.DocumentAllowedClientDetails, opt => opt.MapFrom(src =>
                           src.DocumentAllowedClientDetails.Select(x => x.DocumentAllowedClients.Code)))
                .ReverseMap();

            CreateMap<DocumentFormatDetailDto, DocumentFormatDetail>()
                .ForPath(dest => dest.DocumentFormat.DocumentFormatType.ContentType, opt => opt.MapFrom(src => src.FormatContentType))
                .ForPath(dest => dest.DocumentFormat.DocumentFormatType.Code, opt => opt.MapFrom(src => src.FormatType))
                .ForPath(dest => dest.DocumentFormat.DocumentSize.KiloBytes, opt => opt.MapFrom(src => src.Size)).ReverseMap();

            CreateMap<DocumentUpload, DocumentUploadDto>()
                        .ForMember(dest => dest.DocumentAllowedClientDetails, opt => opt.MapFrom(src =>
                           src.DocumentAllowedClientDetails.Select(x => x.DocumentAllowedClients.Code))).ReverseMap();

            CreateMap<EntityPropertyDto, DocumentEntityProperty>()
                .ForPath(dest => dest.EntityProperty.EntityPropertyValue.Data, opt => opt.MapFrom(src => src.EntityPropertyValue))
                .ForPath(dest => dest.EntityProperty.Code, opt => opt.MapFrom(src => src.Code)).ReverseMap();

            CreateMap<NoteDto, DocumentInstanceNote>()
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note)).ReverseMap();

            CreateMap<DocumentDysDto, DocumentDys>()
                .ForMember(dest => dest.ReferenceName, opt => opt.MapFrom(src => src.ReferenceName))
                .ForMember(dest => dest.ReferenceId, opt => opt.MapFrom(src => src.ReferenceId))
                .ForMember(dest => dest.ReferenceKey, opt => opt.MapFrom(src => src.ReferenceKey))
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields))
                .ForMember(dest => dest.TitleFields, opt => opt.MapFrom(src => src.TitleFields))
                .ReverseMap();

            CreateMap<DocumentTsizlDto, DocumentTsizl>()
                .ForMember(dest => dest.EngagementKind, opt => opt.MapFrom(src => src.EngagementKind))
                .ReverseMap();

            CreateMap<DocumentDefinitionDto, DocumentDefinition>()
                .ForMember(dest => dest.DocumentDefinitionLanguageDetails, opt => opt.MapFrom(src => src.MultilanguageText))
                .ForMember(dest => dest.DocumentEntityPropertys, opt => opt.MapFrom(src => src.EntityProperties))
                .ForMember(dest => dest.DocumentOperations, opt => opt.MapFrom(src => src.DocumentOperations))
                .ForMember(dest => dest.DocumentOnlineSing, opt => opt.MapFrom(src => src.DocumentOnlineSing))
                .ForMember(dest => dest.DocumentUpload, opt => opt.MapFrom(src => src.DocumentUpload))
                .ForMember(dest => dest.DocumentTagsDetails, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.DocumentDys, opt => opt.MapFrom(src => src.DocumentDys))
                .ForMember(dest => dest.DocumentTsizl, opt => opt.MapFrom(src => src.DocumentTsizl))
                .ReverseMap();

            CreateMap<DocumentGroup, DocumentGroupDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.DocumentDefinitions, opt => opt.MapFrom(src =>
                    src.DocumentGroupDetails.Select(x => x.DocumentDefinition)))
                .ForMember(dest => dest.MultilanguageText, opt => opt.MapFrom(src => src.DocumentGroupLanguageDetail))
                .ReverseMap();


            CreateMap<DocumentMetadataInputDto, EntityProperty>()
                .ForPath(dest => dest.EntityPropertyValue.Data, opt => opt.MapFrom(src => src.EntityPropertyValue))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code)).ReverseMap();

        }
    }
}
