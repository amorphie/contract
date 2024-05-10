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


var builder = WebApplication.CreateBuilder(args);
await builder.Configuration.AddVaultSecrets("contract-secretstore", new[] { "contract-secretstore" });

var postgreSql = builder.Configuration["contractdb"];

builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("amorphie.contract.v1", new OpenApiInfo { Title = "amorphie.contract.v1", Version = "v1" });
    c.SwaggerDoc("amorphie.contract.admin", new OpenApiInfo { Title = "amorphie.contract.admin", Version = "v1" });
    c.OperationFilter<AddRequiredHeaderParameter>();
});

builder.Services.AddHealthChecks().AddNpgSql(postgreSql);

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddScoped<IBBTIdentity, FakeIdentity>();

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
builder.Services.AddScoped<IDysProducer, DysProducer>();
builder.Services.AddTransient<IDysIntegrationService, DysIntegrationService>();
builder.Services.AddTransient<IColleteralIntegrationService, ColleteralIntegrationService>();
builder.Services.AddTransient<ICustomerIntegrationService, CustomerIntegrationService>();
builder.Services.AddScoped<ITSIZLProducer, TSIZLProducer>();
builder.Services.AddTransient<ITemplateEngineAppService, TemplateEngineAppService>();

var assemblies = new Assembly[]
                {
                      typeof(DocumentDefinitionValidator).Assembly,
                    //   typeof(MappingDocumentProfile).Assembly,
                };
builder.Services.AddAutoMapper(assemblies);
builder.Services.AddValidatorsFromAssemblyContaining<DocumentDefinitionValidator>(includeInternalTypes: true);
builder.Services.AddScoped<IDocumentService, DocumentService>();

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

builder.Services.AddApplicationServices();

builder.Host.UseSerilog((_, serviceProvider, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(builder.Configuration);

});


var app = builder.Build();
app.UseRouting();
app.UseCors();
app.UseAllElasticApm(app.Configuration);
app.UseCloudEvents();
app.UseApiExceptionHandleMiddlewareExtensions();
app.UseApiHeaderHandleMiddlewareExtensions();
app.MapSubscribeHandler();


using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();

// db.Database.Migrate();
// DbInitializer.Initialize(db); // DB INIT MOCK TESTI ÇALIŞTIRILACAKSA BU SATIRI AÇ DEBUG ET.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/amorphie.contract.v1/swagger.json", "amorphie.contract.v1");
    c.SwaggerEndpoint("/swagger/amorphie.contract.admin/swagger.json", "amorphie.contract.admin");
});

app.MapHealthChecks("/healthz");

// app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.AddRoutes();

app.Run();

