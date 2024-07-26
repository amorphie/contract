using amorphie.core.Identity;
using amorphie.contract.infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using amorphie.core.Extension;
using System.Text.Json.Serialization;
using amorphie.contract.core.Services;
using amorphie.contract.core;
using amorphie.contract.infrastructure.Services;
using amorphie.contract.application;
using Elastic.Apm.NetCoreAll;
using amorphie.contract.infrastructure.Middleware;
using amorphie.contract.infrastructure.Extensions;
using amorphie.contract.application.TemplateEngine;
using amorphie.contract.infrastructure.Services.Kafka;
using amorphie.contract.core.Services.Kafka;
using amorphie.contract.infrastructure.Services.DysSoap;
using amorphie.contract.infrastructure.Services.PusulaSoap;
using amorphie.contract.infrastructure.Services.Refit;
using System.Reflection;
using FluentValidation;
using Serilog;
using Polly.Retry;
using Polly.Extensions.Http;
using Polly.Timeout;
using Polly;
using Refit;
using Microsoft.OpenApi.Models;
using Elastic.Apm.SerilogEnricher;
using Microsoft.Extensions.DependencyInjection;


namespace amorphie.contract.application
{
    public static class RefitClientRegistration
    {
        public static IServiceCollection AddRefitClients(this IServiceCollection services)
        {
            //wait 1s and retry again 3 times when get timeout
            AsyncRetryPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000));

            services
                .AddRefitClient<ITemplateEngineService>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(StaticValuesExtensions.TemplateEngineUrl ??
                                            throw new ArgumentNullException("Parameter is not suplied.", "TemplateEngineUrl")))
                .AddPolicyHandler(retryPolicy);

            services
                .AddRefitClient<ITagService>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(StaticValuesExtensions.TagUrl ??
                                            throw new ArgumentNullException("Parameter is not suplied.", "TagUrl")))
                .AddPolicyHandler(retryPolicy);

            services
                .AddRefitClient<IMessagingGatewayService>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(StaticValuesExtensions.MessagingGatewayUrl ??
                                            throw new ArgumentNullException("Parameter is not suplied.", "MessagingGatewayUrl")))
                .AddPolicyHandler(retryPolicy);

            services
                .AddRefitClient<ICustomerApiService>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(StaticValuesExtensions.CustomerApiUrl ??
                                            throw new ArgumentNullException("Parameter is not suplied.", "CustomerApiUrl")))
                .AddPolicyHandler(retryPolicy);

            return services;
        }
    }
}