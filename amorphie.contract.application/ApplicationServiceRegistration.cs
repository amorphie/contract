using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace amorphie.contract.application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IDocumentAppService, DocumentAppService>();

            return services;
        }
    }
}