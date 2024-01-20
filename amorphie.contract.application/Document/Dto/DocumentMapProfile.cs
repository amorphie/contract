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


        }
    }
}
