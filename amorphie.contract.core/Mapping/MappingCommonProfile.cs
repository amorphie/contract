using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using amorphie.contract.core.Entity.Common;

namespace amorphie.contract.core.Mapping
{
    public class MappingCommonProfile:Profile
    {
        public MappingCommonProfile()
        {
             CreateMap<LanguageType, LanguageType>().ReverseMap();
             CreateMap<MultiLanguage, MultiLanguage>().ReverseMap();
        }
    }
}