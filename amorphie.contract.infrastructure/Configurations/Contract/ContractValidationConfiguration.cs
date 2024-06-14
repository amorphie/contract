using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractValidationConfiguration : ConfigurationBaseAuditEntity<ContractValidation>,
     IEntityTypeConfiguration<ContractValidation>
    {
        public void Configure(EntityTypeBuilder<ContractValidation> builder)
        {
            builder.Navigation(k => k.Validations).AutoInclude();
        }
    }
}