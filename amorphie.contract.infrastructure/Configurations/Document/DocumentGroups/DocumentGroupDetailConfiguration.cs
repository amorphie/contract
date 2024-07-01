using amorphie.contract.core.Entity.Document.DocumentGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.DocumentGroups
{
    public class DocumentGroupDetailConfiguration : ConfigurationBaseAuditEntity<DocumentGroupDetail>,
         IEntityTypeConfiguration<DocumentGroupDetail>
    {
        public void Configure(EntityTypeBuilder<DocumentGroupDetail> builder)
        {
            builder.Navigation(k => k.DocumentDefinition).AutoInclude();
        }
    }
}