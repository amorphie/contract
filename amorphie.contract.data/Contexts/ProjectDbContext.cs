using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore.Design;
using amorphie.core.Repository;
using amorphie.core.Identity;

namespace amorphie.contract.data.Contexts;

/*
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
*/
public class ProjectDbContext : BBTDbContext
{
    /// <summary>
    /// in constructor we get IConfiguration, parallel to more than one db
    /// we can create migration.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration, IBBTIdentity _bbtIdentity)
        : base(options,_bbtIdentity)
    {
        Configuration = configuration;
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

    
}
