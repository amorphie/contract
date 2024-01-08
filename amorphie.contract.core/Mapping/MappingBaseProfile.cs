using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Model;
using amorphie.contract.core.Model.Contract;
using AutoMapper;

namespace amorphie.contract.core.Mapping
{
    public class MappingBaseProfile : Profile
    {
        public MappingBaseProfile()
        {
            CreateMap<MultiLanguage, MultilanguageTextModel>().ConstructUsing(x => new MultilanguageTextModel
            {
                Label = x.Name ,
                Language = x.Code
            });
        }
    }
}