using System.Reflection;
using amorphie.contract.application.Contract;
using amorphie.contract.application.ConverterFactory;
using amorphie.contract.application.Customer;
using amorphie.contract.core.Services;
using amorphie.contract.infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace amorphie.contract.application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IDocumentDefinitionAppService, DocumentDefinitionAppService>();
            services.AddTransient<IContractAppService, ContractAppService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<ICustomerAppService, CustomerAppService>();
            services.AddTransient<IDocumentAppService, DocumentAppService>();
            services.AddTransient<IDocumentDysAppService, DocumentDysAppService>();
            services.AddTransient<IUserSignedContractAppService, UserSignedContractAppService>();

            services.AddScoped<IFileContentProvider, ByteArrayConverter>();
            services.AddScoped<IFileContentProvider, ZeebeRenderConverter>();
            services.AddScoped<IFileContentProvider, TemplateRenderConverter>();
            services.AddScoped<IFileContentProvider, DefaultRenderConverter>();
            services.AddScoped<FileConverterFactory>();

            services.AddScoped<IPdfManager, ITextPdfManager>();

            return services;
        }
    }
}