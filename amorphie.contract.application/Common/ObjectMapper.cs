using amorphie.contract.application.Contract;
using amorphie.contract.application.Customer.Dto;
using AutoMapper;

namespace amorphie.contract.application
{
    public static class ObjectMapperApp
    {
        private static readonly Lazy<IMapper> lazy = new(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DocumentMapProfile>();
            cfg.AddProfile<ContractMapProfile>();
            cfg.AddProfile<CustomerMapProfile>();
            cfg.AddProfile<ContractInstanceMapProfile>();
        });

        return config.CreateMapper();
    });
        public static IMapper Mapper => lazy.Value;
    }

}