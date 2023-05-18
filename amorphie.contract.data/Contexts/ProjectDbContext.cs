using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore.Design;
namespace amorphie.contract.data.Contexts;

class ProjectDbContextFactory : IDesignTimeDbContextFactory<ProjectDbContext>
{
    public ProjectDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ProjectDbContext>();

        var connStr = "Host=localhost:5432;Database=contract;Username=postgres;Password=123321";
        builder.UseNpgsql(connStr);
        return new ProjectDbContext(builder.Options);
    }
}
public class ProjectDbContext : DbContext
{
    /// <summary>
    /// in constructor we get IConfiguration, parallel to more than one db
    /// we can create migration.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public ProjectDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    /// <summary>
    /// Let's also implement the general version.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>


    public DbSet<Language> Language { get; set; }
    public DbSet<Document> Document { get; set; }
    public DbSet<DocumentContent> DocumentContent { get; set; }
    public DbSet<DocumentDefinition> DocumentDefinition { get; set; }
    public DbSet<DocumentGroup> DocumentGroup { get; set; }
    public DbSet<DocumentDefinitionGroupDetail> DocumentGroupDetail { get; set; }
    public DbSet<DocumentTemplate> DocumentTemplate { get; set; }
    public DbSet<DocumentType> DocumentType { get; set; }
    public DbSet<DocumentVersions> DocumentVersions { get; set; }

    protected IConfiguration Configuration { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var connStr = "Host=localhost;Port=5432;Database=TestDb;Username=postgres;Password=123321";
        if (!optionsBuilder.IsConfigured)
        {
            base.OnConfiguring(optionsBuilder.UseNpgsql(connStr)
                .EnableSensitiveDataLogging());
        }
    }
}
