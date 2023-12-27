using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentDefinitionConfiguration : ConfigurationBase<DocumentDefinition>,
    IEntityTypeConfiguration<DocumentDefinition>
    {
        public void Configure(EntityTypeBuilder<DocumentDefinition> builder)
        {
            var list = new List<string>
            {
                "DocumentDefinitionLanguageDetails",
                "DocumentEntityPropertys",
                "DocumentTagsDetails",
                "DocumentUpload",
                "DocumentOnlineSing",
                "DocumentOptimize",
                "DocumentOperations",
            };
            NavigationBuilderAutoInclude(builder, list);
            // builder
            // .HasKey(c => new
            // {
            //     c.Code,
            //     c.Semver
            // });
            //  builder.HasIndex(e => new {e.Code,e.Semver}).IsUnique();
        }

    }
}