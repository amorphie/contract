using amorphie.core.Identity;
using amorphie.core.Repository;
using amorphie.contract.data.Contexts;
using Microsoft.EntityFrameworkCore;
using amorphie.core.Extension;
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

builder.Services.AddScoped<IBBTIdentity, FakeIdentity>();
builder.Services.AddScoped(typeof(IBBTRepository <,>), typeof(BBTRepository <,>));

builder.Services.AddDbContext<ProjectDbContext>
    (options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddRoutes();

app.Run();

