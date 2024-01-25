using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Model;
using AutoMapper;

namespace amorphie.contract.core.Mapping
{
    public class MappingBaseProfile : Profile
    {
        public MappingBaseProfile()
        {
            CreateMap<MultiLanguage, MultilanguageTextModel>().ConstructUsing(x => new MultilanguageTextModel
            {
                Label = x.Name,
                Language = x.Code
            });
        }
    }
}