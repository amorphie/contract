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
            cfg.AddProfile<MappingDocumentProfile>();
        });

        return config.CreateMapper();
    });
        public static IMapper Mapper => lazy.Value;
    }

}