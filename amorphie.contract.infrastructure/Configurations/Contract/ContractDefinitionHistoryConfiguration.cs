
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractDefinitionHistoryConfiguration : ConfigurationBaseAudiEntity<ContractDefinitionHistory>,
     IEntityTypeConfiguration<ContractDefinitionHistory>
    {
        public virtual void Configure(EntityTypeBuilder<ContractDefinitionHistory> builder)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            builder.Property(e => e.ContractDefinitionHistoryModel)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, settings),
                    v => JsonConvert.DeserializeObject<ContractDefinitionHistoryModel>(v) 
                );
        }
    }
}