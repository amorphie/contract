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
                "DocumentDefinitionDys",
                "DocumentDefinitionTsizl"
            };
            NavigationBuilderAutoInclude(builder, list);
            //

            builder.HasIndex(x => new
            {
                x.Code,
                x.Semver
            }).IsUnique();
        }

    }
}