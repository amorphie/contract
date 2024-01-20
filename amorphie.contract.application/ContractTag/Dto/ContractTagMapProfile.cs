using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Contract;
using AutoMapper;

namespace amorphie.contract.application
{
    public class ContractTagMapProfile : Profile
    {
        public ContractTagMapProfile()
        {
            CreateMap<TagDto, Tag>().ReverseMap();

            CreateMap<ContractTagDto, ContractTag>();

        }
    }
}
