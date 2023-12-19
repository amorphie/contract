using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore.Design;
using amorphie.core.Identity;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.core.Entity.EAV;
using amorphie.contract.core.Entity.Proxy;
using amorphie.contract.core.Entity;

namespace amorphie.contract.data.Contexts;

// class ProjectDbContextFactory : IDesignTimeDbContextFactory<ProjectDbContext>
// {
//     //lazy loading true
//     //lazy loading false, eğer alt bileşenleri getirmek istiyorsak include kullanmamız lazım,eager loading
//     private readonly IConfiguration _configuration;

//     public ProjectDbContextFactory() { }

//     public ProjectDbContextFactory(IConfiguration configuration)
//     {
//         _configuration = configuration;
//     }

//     // public ProjectDbContext CreateDbContext(string[] args)
//     // {
//     //     var builder = new DbContextOptionsBuilder<ProjectDbContext>();
//     //     // var test = _configuration["STATE_STORE"];
//     //     // System.Console.WriteLine("Test: " + test);


//     //     var connStr = "Host=localhost:5432;Database=contract;Username=postgres;Password=123321";
//     //     builder.UseNpgsql(connStr);
//     //     builder.EnableSensitiveDataLogging();
//     //     return new ProjectDbContext(builder.Options,null,null);
//     // }
// }

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

    /// <summary>
    /// Let's also implement the general version.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>


    #region Common

    // public DbSet<Callback> Callback { get; set; }
    public DbSet<Tag> Tag { get; set; }
    public DbSet<LanguageType> LanguageType { get; set; }
    public DbSet<MultiLanguage> MultiLanguage { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<Validation> Validation { get; set; }
    public DbSet<ValidationDecision> ValidationDecision { get; set; }
    public DbSet<TemplateRender> TemplateRender { get; set; }
    #endregion
    #region Contract

    public DbSet<ContractDefinition> ContractDefinition { get; set; }
    public DbSet<ContractDocumentDetail> ContractDocumentDetail { get; set; }
    public DbSet<ContractDocumentGroupDetail> ContractDocumentGroupDetail { get; set; }
    public DbSet<ContractEntityProperty> ContractEntityProperty { get; set; }
    public DbSet<ContractTag> ContractTag { get; set; }
    public DbSet<ContractValidation> ContractValidation { get; set; }



    #endregion

    #region Document
    public DbSet<Document> Document { get; set; }
    public DbSet<DocumentAllowedClient> DocumentAllowedClient { get; set; }
    public DbSet<DocumentAllowedClientDetail> DocumentAllowedClientDetail { get; set; }

    public DbSet<DocumentContent> DocumentContent { get; set; }
    public DbSet<DocumentDefinition> DocumentDefinition { get; set; }
    public DbSet<DocumentDefinitionLanguageDetail> DocumentDefinitionLanguageDetail { get; set; }

    public DbSet<DocumentEntityProperty> DocumentEntityProperty { get; set; }
    #region  DocumentGroup
    public DbSet<DocumentGroup> DocumentGroup { get; set; }
    public DbSet<DocumentGroupDetail> DocumentGroupDetail { get; set; }
    #endregion
    public DbSet<DocumentFormat> DocumentFormat { get; set; }
    public DbSet<DocumentFormatDetail> DocumentFormatDetail { get; set; }
    public DbSet<DocumentFormatType> DocumentFormatType { get; set; }


    public DbSet<DocumentOperations> DocumentOperations { get; set; }
    public DbSet<DocumentOptimize> DocumentOptimize { get; set; }
    public DbSet<DocumentOptimizeType> DocumentOptimizeType { get; set; }

    public DbSet<DocumentSize> DocumentSize { get; set; }
    public DbSet<DocumentTagsDetail> DocumentTagsDetail { get; set; }

    public DbSet<DocumentTemplate> DocumentTemplate { get; set; }
    public DbSet<DocumentTemplateDetail> DocumentTemplateDetail { get; set; }

    public DbSet<DocumentUpload> DocumentUpload { get; set; }


    #endregion
    public DbSet<EntityProperty> EntityProperty { get; set; }
    // public DbSet<EntityPropertyType> EntityPropertyType { get; set; }
    public DbSet<EntityPropertyValue> EntityPropertyValue { get; set; }
    public DbSet<Customer> Customer { get; set; }

    protected IConfiguration Configuration { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // modelBuilder.Entity<DocumentDefinition>().Navigation(s => s.BaseStatus).AutoInclude();
        base.OnModelCreating(modelBuilder);

    }


}
