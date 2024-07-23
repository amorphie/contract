using System.Text.Json;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentMigrationDysDocumentTagConfiguration : IEntityTypeConfiguration<DocumentMigrationDysDocumentTag>
    {
        public void Configure(EntityTypeBuilder<DocumentMigrationDysDocumentTag> builder)
        {
            builder.Property(k => k.TagId).HasMaxLength(350);

            builder.HasIndex(x => x.DocId).IsUnique();
            builder.HasIndex(x => x.TagId);

            builder.Property(k => k.TagValues).HasColumnType("jsonb").HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions(JsonSerializerDefaults.General)),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions(JsonSerializerDefaults.General))!);

        }
    }

}
