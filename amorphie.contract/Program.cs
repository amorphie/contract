using amorphie.core.Identity;
using amorphie.contract.infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using amorphie.core.Extension;
using amorphie.contract.core.Mapping;
using System.Reflection;
using FluentValidation;
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

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IBBTIdentity, FakeIdentity>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var settings = builder.Configuration.Get<AppSettings>();
StaticValuesExtensions.SetStaticValues(settings);
builder.Services.AddSingleton<IMinioService, MinioService>();
builder.Services.AddTransient<IDysProducer, DysProducer>();

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

builder.Services.AddApplicationServices();

builder.AddSeriLog();
builder.Services.AddDaprClient();

var app = builder.Build();
app.UseAllElasticApm(builder.Configuration);

app.UseCors();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();

// db.Database.Migrate();
// DbInitializer.Initialize(db); // DB INIT MOCK TESTI ÇALIŞTIRILACAKSA BU SATIRI AÇ DEBUG ET.
app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection();
app.UseExceptionHandleMiddleware();
app.AddRoutes();

app.Run();

