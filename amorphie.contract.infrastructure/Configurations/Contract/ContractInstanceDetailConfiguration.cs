using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractInstanceDetailConfiguration : ConfigurationBaseAudiEntity<ContractInstanceDetail>,
 IEntityTypeConfiguration<ContractInstanceDetail>
    {
        public override void Configure(EntityTypeBuilder<ContractInstanceDetail> builder)
        {
            builder.HasIndex(x => x.DocumentInstanceId);

            builder.Property(k => k.DocumentInstanceId).IsRequired();

            builder.Property(k => k.ContractInstanceId).IsRequired();

        }
    }
}