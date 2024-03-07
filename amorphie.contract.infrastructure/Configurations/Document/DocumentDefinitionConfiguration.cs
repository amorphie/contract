using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
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
                "DocumentDys",
                "DocumentTsizl"
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