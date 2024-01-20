using AutoMapper;

namespace amorphie.contract.application
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DocumentMapProfile>();
        });

        return config.CreateMapper();
    });
        public static IMapper Mapper => lazy.Value;
    }

}