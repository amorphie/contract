using System.Reflection;
using amorphie.contract.application.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace amorphie.contract.application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddTransient<IDocumentAppService, DocumentAppService>();
            services.AddTransient<IDocumentDefinitionAppService, DocumentDefinitionAppService>();
            services.AddTransient<IContractAppService, ContractAppService>();

            return services;
        }
    }
}