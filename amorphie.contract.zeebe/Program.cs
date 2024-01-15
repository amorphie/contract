using amorphie.contract.data.Contexts;
using amorphie.contract.core;
using amorphie.contract.zeebe.Modules;
using amorphie.contract.zeebe.Modules.ZeebeDocumentDef;
using amorphie.contract.zeebe.Service.Minio;
using amorphie.contract.zeebe.Services;
using amorphie.contract.zeebe.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddSwaggerGen();
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
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var settings = builder.Configuration.Get<AppSettings>();
StaticValuesExtensions.SetStaticValues(settings);


builder.Services.AddSingleton<IMinioService, MinioService>();
builder.Services.AddScoped<IDocumentDefinitionService, DocumentDefinitionService>();
builder.Services.AddScoped<IDocumentGroupDefinitionService, DocumentGroupDefinitionService>();
builder.Services.AddScoped<IContractDefinitionService, ContractDefinitionService>();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();

// db.Database.Migrate();
// sssss
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
app.MapZeebeDocumentUploadEndpoints();
app.MapZeebeDocumentDefinitionEndpoints();
app.MapZeebeContractDefinitionEndpoints();
app.MapZeebeDocumentGroupDefinitionEndpoints();
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
app.Run();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
