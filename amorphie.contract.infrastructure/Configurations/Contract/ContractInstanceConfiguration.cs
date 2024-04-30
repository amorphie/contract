using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractInstanceConfiguration : ConfigurationBaseAudiEntity<ContractInstance>,
     IEntityTypeConfiguration<ContractInstance>
    {
        public override void Configure(EntityTypeBuilder<ContractInstance> builder)
        {
            builder.HasIndex(x => x.CustomerId);

            builder.Property(k => k.ContractCode).IsRequired().HasMaxLength(1000);

        }
    }
}