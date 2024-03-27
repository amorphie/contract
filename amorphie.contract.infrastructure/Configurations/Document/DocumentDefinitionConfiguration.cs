using System.Text.Json;
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

            builder.Property(k => k.Titles).HasColumnType("jsonb").HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions(JsonSerializerDefaults.General)),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions(JsonSerializerDefaults.General))!);

        }

    }
}