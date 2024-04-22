using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractDocumentGroupDetailConfiguration : ConfigurationBaseAuditEntity<ContractDocumentGroupDetail>,
     IEntityTypeConfiguration<ContractDocumentGroupDetail>
    {
        public void Configure(EntityTypeBuilder<ContractDocumentGroupDetail> builder)
        {
            builder.Navigation(k => k.DocumentGroup).AutoInclude();
        }
    }
}