using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.EAV;
using AutoMapper;

namespace amorphie.contract.core.Mapping
{
    public class MappingEAVProfile : Profile
    {
        public MappingEAVProfile()
        {
            CreateMap<EntityProperty, EntityProperty>().ReverseMap();
            //  CreateMap<EntityPropertyType, EntityPropertyType>().ReverseMap();
            CreateMap<EntityPropertyValue, EntityPropertyValue>().ReverseMap();


        }
    }
}