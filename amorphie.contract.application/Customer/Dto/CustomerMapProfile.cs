using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Contract;
using AutoMapper;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.core.Base;
using amorphie.contract.core;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Model.Documents;

namespace amorphie.contract.application.Customer.Dto
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {

            CreateMap<ContractDefinition, CustomerContractDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.CustomerContractDocuments, opt => opt.MapFrom(src => src.ContractDocumentDetails))
                .ForMember(dest => dest.CustomerContractDocumentGroups, opt => opt.MapFrom(src => src.ContractDocumentGroupDetails))
                .ReverseMap();

            CreateMap<DocumentDefinition, CustomerContractDocumentDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
               .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Semver))
               .ForMember(dest => dest.Render, opt => opt.MapFrom(src => src.DocumentOnlineSign != null))
               .ForMember(dest => dest.OnlineSign, opt => opt.MapFrom(src => src.DocumentOnlineSign))
               .ForMember(dest => dest.ApprovalDate, opt => opt.MapFrom(src => src.CreatedAt))
               .ReverseMap();

            CreateMap<ContractDocumentDetail, CustomerContractDocumentDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DocumentDefinition.Id))
               // [LANG]   .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.DocumentDefinition.DocumentDefinitionLanguageDetails.FirstOrDefault().MultiLanguage.Name))
               .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.DocumentDefinition.Code))
               .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.DocumentDefinition.Semver))
               .ForMember(dest => dest.Render, opt => opt.MapFrom(src => src.DocumentDefinition.DocumentOnlineSign != null))
               .ForMember(dest => dest.Required, opt => opt.MapFrom(src => src.Required))
               .ForMember(dest => dest.OnlineSign, opt => opt.MapFrom(src => src.DocumentDefinition.DocumentOnlineSign))
               .ForMember(dest => dest.ApprovalDate, opt => opt.MapFrom(src => src.CreatedAt))
               .ForMember(dest => dest.Titles, opt => opt.MapFrom(src => src.DocumentDefinition.Titles)).ReverseMap();

            CreateMap<DocumentOnlineSign, OnlineSignDto>()
                .ForMember(dest => dest.AllovedClients, opt => opt.MapFrom(src => src.DocumentAllowedClientDetails.Select(x => x.DocumentAllowedClients.Code)))
                .ForMember(dest => dest.ScaRequired, opt => opt.MapFrom(src => src.Required))
                .ForMember(dest => dest.DocumentModelTemplate ,opt=> opt.MapFrom(src=>src.Templates) )
                .ReverseMap();

        
            CreateMap<Template, DocumentTemplateDto>()
                         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Code))
                         .ForMember(dest => dest.MinVersion, opt => opt.MapFrom(src => src.Version))
                         .ReverseMap();
            //CreateMap<DocumentGroup, CustomerContractDocumentGroupDto>()
            //   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //   .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            //   .ForMember(dest => dest.CustomerContractGroupDocuments, opt => opt.MapFrom(src => src.DocumentGroupDetails.Select(x => x.DocumentDefinition)))
            //   .ForMember(dest => dest.MultiLanguageText, opt => opt.MapFrom(src => src.DocumentGroupLanguageDetail))
            //   .ReverseMap();

            CreateMap<ContractDocumentGroupDetail, CustomerContractDocumentGroupDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DocumentGroup.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.DocumentGroup.Code))
                .ForMember(dest => dest.CustomerContractGroupDocuments, opt => opt.MapFrom(src => src.DocumentGroup.DocumentGroupDetails.Select(x => x.DocumentDefinition)))
                .ForMember(dest => dest.AtLeastRequiredDocument, opt => opt.MapFrom(src => src.AtLeastRequiredDocument))
                .ForMember(dest => dest.Required, opt => opt.MapFrom(src => src.Required))
                .ForMember(dest => dest.DocumentGroupStatus, opt => opt.MapFrom(src => AppConsts.NotValid))
                //[LANG] .ForMember(dest => dest.MultiLanguageText, opt => opt.MapFrom(src => src.DocumentGroup.DocumentGroupLanguageDetail))
                .ForMember(dest => dest.Titles, opt => opt.MapFrom(src => src.DocumentGroup.Titles))
                .ReverseMap();
        }
    }
}

