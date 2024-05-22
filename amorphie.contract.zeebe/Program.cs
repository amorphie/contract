using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core;
using amorphie.contract.zeebe.Modules;
using amorphie.contract.zeebe.Modules.ZeebeDocumentDef;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.application;
using amorphie.core.Extension;
using Elastic.Apm.NetCoreAll;
using amorphie.contract.core.Services;
using amorphie.contract.infrastructure.Services;
using amorphie.contract.infrastructure.Middleware;
using amorphie.contract.infrastructure.Extensions;
using amorphie.contract.application.Contract;
using System.Text.Json;
using amorphie.contract.zeebe.Services;
using amorphie.contract.core.Services.Kafka;
using amorphie.contract.infrastructure.Services.Kafka;
using amorphie.contract.infrastructure.Services.DysSoap;
using amorphie.contract.infrastructure.Services.PusulaSoap;
using Serilog;
using amorphie.contract.application.TemplateEngine;
using Refit;
using amorphie.contract.infrastructure.Services.Refit;
using Polly.Timeout;
using Polly.Retry;
using Polly.Extensions.Http;
using Polly;
using Elastic.Apm.SerilogEnricher;


var builder = WebApplication.CreateBuilder(args);
await builder.Configuration.AddVaultSecrets("contract-zeebe-secretstore", new[] { "contract-secretstore" });
var postgreSql = builder.Configuration["contractdb"];

builder.Services.AddDaprClient();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AddRequiredHeaderParameter>();
});

builder.Services.AddHealthChecks().AddNpgSql(postgreSql);

builder.Logging.ClearProviders();

builder.Host.UseSerilog((_, serviceProvider, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.WithElasticApmCorrelationInfo();

});




builder.Services.AddDbContext<ProjectDbContext>
    (options => options.UseNpgsql(postgreSql));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddSingleton<IConfigurationRoot>(provider => builder.Configuration);

var settings = builder.Configuration.Get<AppSettings>();
StaticValuesExtensions.SetStaticValues(settings);

//wait 1s and retry again 3 times when get timeout
AsyncRetryPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .Or<TimeoutRejectedException>()
    .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000));

builder.Services
    .AddRefitClient<ITemplateEngineService>()
    .ConfigureHttpClient(c =>
        c.BaseAddress = new Uri(StaticValuesExtensions.TemplateEngineUrl ??
                                throw new ArgumentNullException("Parameter is not suplied.", "TemplateEngineUrl")))
    .AddPolicyHandler(retryPolicy);


builder.Services.AddSingleton<IMinioService, MinioService>();
builder.Services.AddScoped<IDocumentDefinitionService, DocumentDefinitionService>();
builder.Services.AddScoped<IDocumentGroupDefinitionService, DocumentGroupDefinitionService>();
builder.Services.AddScoped<IContractDefinitionService, ContractDefinitionService>();
builder.Services.AddScoped<IContractAppService, ContractAppService>();
builder.Services.AddScoped<IDysProducer, DysProducer>();
builder.Services.AddTransient<IDysIntegrationService, DysIntegrationService>();
builder.Services.AddTransient<IColleteralIntegrationService, ColleteralIntegrationService>();
builder.Services.AddTransient<ICustomerIntegrationService, CustomerIntegrationService>();
builder.Services.AddScoped<ITSIZLProducer, TSIZLProducer>();
builder.Services.AddTransient<ITemplateEngineAppService, TemplateEngineAppService>();



builder.Services.AddApplicationServices();
builder.Services.AddSingleton(new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
});

var app = builder.Build();
app.UseAllElasticApm(builder.Configuration);
app.UseZeebeExceptionHandleMiddlewareExtensions();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();

// db.Database.Migrate();
// sssss
app.UseSwagger();
app.UseSwaggerUI();

app.MapHealthChecks("/healthz");

app.UseHttpsRedirection();


app.MapZeebeDocumentUploadEndpoints();
app.MapZeebeDocumentDefinitionEndpoints();
app.MapZeebeContractDefinitionEndpoints();
app.MapZeebeDocumentGroupDefinitionEndpoints();

app.MapZeebeContractInstanceEndpoints();
app.MapZeebeRenderOnlineSignEndpoints();


app.Run();

