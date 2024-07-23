using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.core.Entity.Proxy;
using amorphie.contract.core.Entity;
using System.Linq.Expressions;
using amorphie.contract.core.Entity.Base;
using Microsoft.EntityFrameworkCore.Query;
using amorphie.contract.infrastructure.Extensions;

namespace amorphie.contract.infrastructure.Contexts;

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

    public DbSet<Tag> Tag { get; set; }
    public DbSet<LanguageType> LanguageType { get; set; }
    public DbSet<MultiLanguage> MultiLanguage { get; set; }
    public DbSet<Validation> Validation { get; set; }
    public DbSet<ValidationDecision> ValidationDecision { get; set; }
    public DbSet<TemplateRender> TemplateRender { get; set; }
    #endregion

    #region Contract

    public DbSet<ContractDefinition> ContractDefinition { get; set; }
    public DbSet<ContractDocumentDetail> ContractDocumentDetail { get; set; }
    public DbSet<ContractDocumentGroupDetail> ContractDocumentGroupDetail { get; set; }
    public DbSet<ContractTag> ContractTag { get; set; }
    public DbSet<ContractValidation> ContractValidation { get; set; }
    public DbSet<ContractDefinitionHistory> ContractDefinitionHistory { get; set; }
    public DbSet<UserSignedContract> UserSignedContract { get; set; }
    public DbSet<UserSignedContractDetail> UserSignedContractDetail { get; set; }
    public DbSet<ContractCategory> ContractCategory { get; set; }
    public DbSet<ContractCategoryDetail> ContractCategoryDetail { get; set; }

    #endregion

    #region Document
    public DbSet<Document> Document { get; set; }
    public DbSet<DocumentInstanceNote> DocumentInstanceNote { get; set; }
    public DbSet<DocumentAllowedClient> DocumentAllowedClient { get; set; }
    public DbSet<DocumentAllowedClientDetail> DocumentAllowedClientDetail { get; set; }

    public DbSet<DocumentContent> DocumentContent { get; set; }
    public DbSet<DocumentDefinition> DocumentDefinition { get; set; }
    public DbSet<DocumentOnlineSign> DocumentOnlineSign { get; set; }
    public DbSet<DocumentTsizl> DocumentTsizls { get; set; }
    public DbSet<DocumentDys> DocumentDys { get; set; }

    #region  DocumentGroup
    public DbSet<DocumentGroup> DocumentGroup { get; set; }
    public DbSet<DocumentGroupDetail> DocumentGroupDetail { get; set; }
    public DbSet<DocumentGroupHistory> DocumentGroupHistory { get; set; }
    #endregion
    public DbSet<DocumentFormat> DocumentFormat { get; set; }
    public DbSet<DocumentFormatDetail> DocumentFormatDetail { get; set; }
    public DbSet<DocumentFormatType> DocumentFormatType { get; set; }


    public DbSet<DocumentOperations> DocumentOperations { get; set; }
    public DbSet<DocumentOptimize> DocumentOptimize { get; set; }
    public DbSet<DocumentOptimizeType> DocumentOptimizeType { get; set; }

    public DbSet<DocumentSize> DocumentSize { get; set; }
    public DbSet<DocumentTagsDetail> DocumentTagsDetail { get; set; }


    public DbSet<DocumentUpload> DocumentUpload { get; set; }


    #endregion

    public DbSet<Customer> Customer { get; set; }
    public DbSet<CustomerCommunication> CustomerCommunication { get; set; }
    public DbSet<Contract> Contract { get; set; }
    protected IConfiguration Configuration { get; }

    #region Dys Migration

    public DbSet<DocumentMigrationAggregation> DocumentMigrationAggregations { get; set; }
    // public DbSet<DocumentMigrationDysDocument> DocumentMigrationDysDocuments { get; set; }
    public DbSet<DocumentMigrationDysDocumentTag> DocumentMigrationDysDocumentTags { get; set; }
    public DbSet<DocumentMigrationProcessing> DocumentMigrationProcessings { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        Expression<Func<ISoftDelete, bool>> filterExpr = x => !x.IsDeleted;
        foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
        {
            if (mutableEntityType.ClrType.GetInterfaces().Contains(typeof(ISoftDelete)))
            {
                var parameter = Expression.Parameter(mutableEntityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);
                mutableEntityType.SetQueryFilter(lambdaExpression);
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder
             .AddInterceptors(new SoftDeleteInterceptor());

}