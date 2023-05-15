using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using amorphie.contract.core.Entity.Definition;
using amorphie.contract.core.Entity.Common;

namespace amorphie.contract.data;
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
        protected ProjectDbContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;

        }
        public DbSet<Language> Language { get; set; }
        public DbSet<DocumentDefinition> DocumentDefinition { get; set; }
        public DbSet<DocumentDefinitionTemplate> DocumentDefinitionTemplate { get; set; }
        public DbSet<DocumentDefinitionType> DocumentDefinitionType { get; set; }

        protected IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DArchPgContext"))
                    .EnableSensitiveDataLogging());
            }
        }
    }
