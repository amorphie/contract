using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace amorphie.contract.core.Mapping
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingCommonProfile>();
            cfg.AddProfile<MappingContractProfile>();
            cfg.AddProfile<MappingDocumentProfile>();
            cfg.AddProfile<MappingEAVProfile>();
        });

        return config.CreateMapper();
    });
        public static IMapper Mapper => lazy.Value;
    }

}