using System.Text.Json;
using System.Text.Json.Serialization;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Model.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class DocumentGroupHistoryConfiguration : ConfigurationBaseAudiEntity<DocumentGroupHistory>,
     IEntityTypeConfiguration<DocumentGroupHistory>
    {
        public virtual void Configure(EntityTypeBuilder<DocumentGroupHistory> builder)
        {
            var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };

                builder.Property(e => e.DocumentGroupHistoryModel)
                    .HasColumnType("jsonb")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, options),
                        v => JsonSerializer.Deserialize<DocumentGroupHistoryModel>(v, options)
                    );
        }
    }
}