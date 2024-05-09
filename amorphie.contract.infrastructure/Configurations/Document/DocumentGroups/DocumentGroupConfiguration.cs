using System.Text.Json;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.DocumentGroups
{
    public class DocumentGroupConfiguration : ConfigurationBase<DocumentGroup>,
         IEntityTypeConfiguration<DocumentGroup>

    {
        public void Configure(EntityTypeBuilder<DocumentGroup> builder)
        {
            builder.Navigation(k => k.DocumentGroupDetails).AutoInclude();
            builder.Property(k => k.Titles).HasColumnType("jsonb").HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions(JsonSerializerDefaults.General)),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions(JsonSerializerDefaults.General))!);

        }
    }
}