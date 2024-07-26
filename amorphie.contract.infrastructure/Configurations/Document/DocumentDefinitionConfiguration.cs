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
            builder.Navigation(k => k.DocumentTagsDetails).AutoInclude();
            builder.Navigation(k => k.DocumentUpload).AutoInclude();
            builder.Navigation(k => k.DocumentOnlineSign).AutoInclude();
            builder.Navigation(k => k.DocumentOptimize).AutoInclude();
            builder.Navigation(k => k.DocumentOperations).AutoInclude();
            builder.Navigation(k => k.DocumentDys).AutoInclude();
            builder.Navigation(k => k.DocumentTsizl).AutoInclude();

            builder.HasIndex(x => new
            {
                x.Code,
                x.Semver
            }).IsUnique();

            builder.Property(k => k.Titles).HasColumnType("jsonb").HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions(JsonSerializerDefaults.General)),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions(JsonSerializerDefaults.General))!);

            builder.OwnsMany(d => d.DefinitionMetadata, builder => { builder.ToJson(); });
        }
    }

}
