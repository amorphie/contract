using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractTagConfiguration : ConfigurationBaseAuditEntity<ContractTag>,
     IEntityTypeConfiguration<ContractTag>
    {
        public void Configure(EntityTypeBuilder<ContractTag> builder)
        {
            builder.Navigation(k => k.Tags).AutoInclude();
        }
    }
}