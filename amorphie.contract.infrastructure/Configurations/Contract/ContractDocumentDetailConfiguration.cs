using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractDocumentDetailConfiguration : ConfigurationBaseAuditEntity<ContractDocumentDetail>,
     IEntityTypeConfiguration<ContractDocumentDetail>
    {
        public void Configure(EntityTypeBuilder<ContractDocumentDetail> builder)
        {
            builder.Navigation(k => k.DocumentDefinition).AutoInclude();
        }
    }
}