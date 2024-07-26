using System.Reflection;
using amorphie.contract.application.Contract;
using amorphie.contract.application.ConverterFactory;
using amorphie.contract.application.Customer;
using amorphie.contract.application.Migration;
using amorphie.contract.application.CustomerApi;
using amorphie.contract.application.MessagingGateway;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.core.Services;
using amorphie.contract.infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using amorphie.contract.infrastructure.Services.DysSoap;
using amorphie.contract.infrastructure.Services.PusulaSoap;
using amorphie.contract.core.Services.Kafka;
using amorphie.contract.infrastructure.Services.Kafka;

namespace amorphie.contract.application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IDocumentDefinitionAppService, DocumentDefinitionAppService>();
            services.AddTransient<IContractAppService, ContractAppService>();
            services.AddTransient<IContractDefinitionAppService, ContractDefinitionAppService>();
            services.AddTransient<IDocumentGroupAppService, DocumentGroupAppService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<ICustomerAppService, CustomerAppService>();
            services.AddTransient<IDocumentAppService, DocumentAppService>();
            services.AddTransient<IDocumentDysAppService, DocumentDysAppService>();
            services.AddTransient<IUserSignedContractAppService, UserSignedContractAppService>();
            services.AddScoped<IContractCategoryAppService, ContractCategoryAppService>();
            services.AddTransient<ITagAppService, TagAppService>();
            services.AddTransient<ITemplateEngineAppService, TemplateEngineAppService>();
            services.AddScoped<IMessagingGatewayAppService, MessagingGatewayAppService>();
            services.AddScoped<ICustomerApiAppService, CustomerApiAppService>();

            services.AddScoped<IFileContentProvider, ByteArrayConverter>();
            services.AddScoped<IFileContentProvider, ZeebeRenderConverter>();
            services.AddScoped<IFileContentProvider, TemplateRenderConverter>();
            services.AddScoped<IFileContentProvider, DefaultRenderConverter>();
            services.AddScoped<IFileContentProvider, HtmlToPdfConverter>();
            services.AddScoped<IFileContentProvider, JpegToPdfConverter>();
            services.AddScoped<IFileContentProvider, TiffToPdfConverter>();
            services.AddScoped<IFileContentProvider, OtherImageToPdfConverter>();
            services.AddScoped<IFileContentProvider, BmpToPdfConverter>();
            services.AddScoped<IFileContentProvider, PngToPdfConverter>();
 
            services.AddScoped<FileConverterFactory>();

            services.AddScoped<IPdfManager, ITextPdfManager>();

            services.AddTransient<IDysMigrationAppService, DysMigrationAppService>();



            services.AddSingleton<IMinioService, MinioService>();
            services.AddTransient<IDysIntegrationService, DysIntegrationService>();
            services.AddTransient<IColleteralIntegrationService, ColleteralIntegrationService>();
            services.AddTransient<ICustomerIntegrationService, CustomerIntegrationService>();
            services.AddScoped<IDysProducer, DysProducer>();
            services.AddScoped<ITSIZLProducer, TSIZLProducer>();
            return services;
        }
    }
}