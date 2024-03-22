using System.Runtime.InteropServices;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.core.Enum;
using AutoMapper;

namespace amorphie.contract.application.Contract
{
    public class ContractInstanceMapProfile : Profile
    {
        public ContractInstanceMapProfile()
        {
            CreateMap<ContractDocumentDetailDto, DocumentInstanceDto>()
                .ForMember(dest => dest.IsRequired, opt => opt.MapFrom(src => src.Required))
                .ForMember(dest => dest.MinVersion, opt => opt.MapFrom(src => src.MinVersion))
                .ForMember(dest => dest.UseExisting, opt => opt.MapFrom(src => src.UseExisting))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.DocumentDefinition.Code))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.DocumentDefinition.Status))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DocumentDefinition.MultilanguageText.FirstOrDefault(x => x.Language == ContractRequestHeader.LangCode).Label 
                    ?? src.DocumentDefinition.MultilanguageText.FirstOrDefault().Label))
                .ForPath(dest => dest.DocumentDetail.OnlineSing, opt => opt.MapFrom(src => src.DocumentDefinition.DocumentOnlineSing))
                .ReverseMap();

            CreateMap<DocumentOnlineSingDto, DocumentInstanceOnlineSingDto>()
                .ForMember(dest => dest.TemplateCode, opt => opt.MapFrom(src =>
                    src.DocumentTemplateDetails.FirstOrDefault(x => x.LanguageType == ContractRequestHeader.LangCode).Code 
                    ?? src.DocumentTemplateDetails.FirstOrDefault().Code))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src =>
                    src.DocumentTemplateDetails.FirstOrDefault(x => x.LanguageType == ContractRequestHeader.LangCode).Version 
                    ?? src.DocumentTemplateDetails.FirstOrDefault().Version))
                .ReverseMap();

            CreateMap<ContractDocumentGroupDetailDto, DocumentGroupInstanceDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ContractDocumentGroup.Status))
                .ForMember(dest => dest.AtLeastRequiredDocument, opt => opt.MapFrom(src => src.AtLeastRequiredDocument))
                .ForMember(dest => dest.Required, opt => opt.MapFrom(src => src.Required))
                .ForPath(dest => dest.DocumentGroupDetailInstanceDto.Code, opt => opt.MapFrom(src => src.ContractDocumentGroup.Code))
                .ForPath(dest => dest.DocumentGroupDetailInstanceDto.Name, opt => opt.MapFrom(src => src.ContractDocumentGroup.MultilanguageText.FirstOrDefault(x => x.Language == ContractRequestHeader.LangCode).Label
                    ?? src.ContractDocumentGroup.MultilanguageText.FirstOrDefault().Label))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.ContractDocumentGroup.MultilanguageText.FirstOrDefault(x => x.Language == ContractRequestHeader.LangCode).Label
                    ?? src.ContractDocumentGroup.MultilanguageText.FirstOrDefault().Label))
                .ForPath(dest => dest.DocumentGroupDetailInstanceDto.DocumentInstances, opt => opt.MapFrom(src => src.ContractDocumentGroup.DocumentDefinitions))
                .ReverseMap();

            CreateMap<DocumentDefinitionDto,DocumentInstanceDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MultilanguageText.FirstOrDefault(x => x.Language == ContractRequestHeader.LangCode).Label 
                    ?? src.MultilanguageText.FirstOrDefault().Label))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForPath(dest => dest.DocumentDetail.OnlineSing, opt => opt.MapFrom(src => src.DocumentOnlineSing))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.MinVersion, opt => opt.MapFrom(src => src.Semver))
                .ReverseMap();
        }
    }


}
