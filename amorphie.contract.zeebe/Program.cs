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


var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration;

Configuration = builder
    .Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddUserSecrets<Program>()
    .Build();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AddRequiredHeaderParameter>();
});

builder.AddSeriLog();

builder.Services.AddDbContext<ProjectDbContext>
    (options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
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
builder.Services.AddDaprClient();

builder.Services.AddSingleton<IConfigurationRoot>(provider => builder.Configuration);

// builder.Services.Configure<JsonSerializerOptions>(options =>
// {
//     options.PropertyNameCaseInsensitive = true;
// });


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var settings = builder.Configuration.Get<AppSettings>();
StaticValuesExtensions.SetStaticValues(settings);


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

app.UseHttpsRedirection();


app.MapZeebeDocumentUploadEndpoints();
app.MapZeebeDocumentDefinitionEndpoints();
app.MapZeebeContractDefinitionEndpoints();
app.MapZeebeDocumentGroupDefinitionEndpoints();

app.MapZeebeContractInstanceEndpoints();
app.MapZeebeRenderOnlineSignEndpoints();


app.Run();

