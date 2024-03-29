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
using System.Reflection;
using FluentValidation;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
await builder.Configuration.AddVaultSecrets("contract-secretstore", new[] { "contract-secretstore" });

var postgreSql = builder.Configuration["contractdb"];

builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AddRequiredHeaderParameter>();
});

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IBBTIdentity, FakeIdentity>();

var settings = builder.Configuration.Get<AppSettings>();
StaticValuesExtensions.SetStaticValues(settings);

builder.Services.AddSingleton<IMinioService, MinioService>();
builder.Services.AddScoped<IDysProducer, DysProducer>();
builder.Services.AddTransient<IDysIntegrationService, DysIntegrationService>();
builder.Services.AddTransient<IColleteralIntegrationService, ColleteralIntegrationService>();
builder.Services.AddTransient<ICustomerIntegrationService, CustomerIntegrationService>();
builder.Services.AddScoped<ITSIZLProducer, TSIZLProducer>();

builder.Services.AddSingleton<ITemplateEngineService, TemplateEngineService>();
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
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.AddRoutes();

app.Run();

