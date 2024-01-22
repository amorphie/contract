using amorphie.contract.core.Entity.Document;
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

            CreateMap<DocumentDefinitionDto, DocumentDefinition>().ReverseMap();

            CreateMap<DocumentDefinitionLanguageDetail, MultilanguageText>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.MultiLanguage.Name))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.MultiLanguage.LanguageType.Code)).ReverseMap();

            CreateMap<DocumentInstanceInputDto, DocumentContent>()
                .ForMember(dest => dest.ContentData, opt => opt.MapFrom(src => src.FileContext.ToString()))
                .ForMember(dest => dest.KiloBytesSize, opt => opt.MapFrom(src => src.FileContext.ToString().Length.ToString()))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.FileType))
                .ForMember(dest => dest.MinioObjectName, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.ContentTransferEncoding, opt => opt.MapFrom(src => src.FileType))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString())).ReverseMap();
        }
    }
}
