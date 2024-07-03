using amorphie.contract.core.Model;
using AutoMapper;

namespace amorphie.contract.application
{
    public class CommonMapProfile : Profile
    {
        public CommonMapProfile()
        {
            CreateMap<Metadata, MetadataDto>().ReverseMap();

        }
    }
}
