using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Model.History;
using AutoMapper;

namespace amorphie.contract.application.Contract
{
    public class ContractMapProfile : Profile
    {
        public ContractMapProfile()
        {

            CreateMap<ContractDocumentDetail, ContractDocumentDetailDto>()
                .ForMember(dest => dest.DocumentDefinition, opt => opt.MapFrom(src => src.DocumentDefinition))
                .ForMember(dest => dest.UseExisting, opt => opt.MapFrom(src => src.UseExisting.ToString()))
                .ForMember(dest => dest.MinVersion, opt => opt.MapFrom(src => src.DocumentDefinition.Semver)).ReverseMap();

            CreateMap<ContractDefinition, ContractDefinitionDto>()
                    .ForPath(dest => dest.ContractDocumentDetails, opt => opt.MapFrom(src => src.ContractDocumentDetails))
                    .ForPath(dest => dest.ContractDocumentGroupDetails, opt => opt.MapFrom(src => src.ContractDocumentGroupDetails))
                    .ReverseMap();

            CreateMap<ContractDocumentGroupDetailDto, ContractDocumentGroupDetail>()
                    .ForMember(dest => dest.DocumentGroup, opt => opt.MapFrom(src => src.ContractDocumentGroup))
                    .ForMember(dest => dest.Required, opt => opt.MapFrom(src => src.Required))
                    .ForMember(dest => dest.AtLeastRequiredDocument, opt => opt.MapFrom(src => src.AtLeastRequiredDocument))
                    .ReverseMap();

            CreateMap<ContractDefinition, ContractDefinitionHistoryModel>()
                    .ReverseMap();

            CreateMap<ContractCategory, ContractCategoryDto>()
                    .ReverseMap();
        }
    }

}
