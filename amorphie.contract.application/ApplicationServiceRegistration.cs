using System.Reflection;
using amorphie.contract.application.Contract;
using amorphie.contract.application.Customer;
using amorphie.contract.data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace amorphie.contract.application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IDocumentAppService, DocumentAppService>();
            services.AddTransient<IDocumentDefinitionAppService, DocumentDefinitionAppService>();
            services.AddTransient<IContractAppService, ContractAppService>();
            services.AddTransient<IDocumentService,DocumentService>();
            services.AddTransient<ICustomerAppService, CustomerAppService>();

            return services;
        }
    }
}