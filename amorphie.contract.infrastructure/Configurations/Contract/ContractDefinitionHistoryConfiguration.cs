
using System.Text.Json;
using System.Text.Json.Serialization;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Model.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractDefinitionHistoryConfiguration : ConfigurationBaseAudiEntity<ContractDefinitionHistory>,
     IEntityTypeConfiguration<ContractDefinitionHistory>
    {
        public virtual void Configure(EntityTypeBuilder<ContractDefinitionHistory> builder)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            builder.Property(e => e.ContractDefinitionHistoryModel)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, options),
                    v => JsonSerializer.Deserialize<ContractDefinitionHistoryModel>(v, options)
                );
        }
    }
}