using System.Reflection;
using amorphie.contract.application.Contract;
using amorphie.contract.application.ConverterFactory;
using amorphie.contract.application.Customer;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.core.Services;
using amorphie.contract.infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using amorphie.contract.infrastructure.Services.Kafka;
using amorphie.contract.core.Services.Kafka;
using amorphie.contract.infrastructure.Services.DysSoap;
using amorphie.contract.infrastructure.Services.PusulaSoap;
using amorphie.contract.application.MessagingGateway;

namespace amorphie.contract.application
{
    public static class CustomServiceRegistration
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddApplicationServices();

            services.AddScoped<IFileContentProvider, ByteArrayConverter>();
            services.AddScoped<IFileContentProvider, ZeebeRenderConverter>();
            services.AddScoped<IFileContentProvider, TemplateRenderConverter>();
            services.AddScoped<IFileContentProvider, DefaultRenderConverter>();
            services.AddScoped<FileConverterFactory>();

            services.AddScoped<IDocumentService, DocumentService>();
            services.AddSingleton<IMinioService, MinioService>();
            services.AddTransient<IDysIntegrationService, DysIntegrationService>();
            services.AddTransient<IColleteralIntegrationService, ColleteralIntegrationService>();
            services.AddTransient<ICustomerIntegrationService, CustomerIntegrationService>();
            services.AddScoped<IDysProducer, DysProducer>();
            services.AddScoped<ITSIZLProducer, TSIZLProducer>();
            services.AddScoped<IPdfManager, ITextPdfManager>();

            return services;
        }
    }
}