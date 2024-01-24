using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core.Entity.Contract;
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
                    .ForMember(dest => dest.ContractDocumentDetails, opt => opt.MapFrom(src => src.ContractDocumentDetails))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                    .ReverseMap();

            CreateMap<ContractDocumentGroupDetailDto,ContractDocumentGroupDetail>()
                    .ForMember(dest => dest.DocumentGroup.DocumentGroupDetails.Select(x=> x.DocumentDefinition), opt => opt.MapFrom(src => src.DocumentDefinitions)).ReverseMap();

        }
    }

}
