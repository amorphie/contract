using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentDysConfiguration : ConfigurationBaseAuditEntity<DocumentDys>,
         IEntityTypeConfiguration<DocumentDys>

    {
        public void Configure(EntityTypeBuilder<DocumentDys> builder)
        {


        }
    }
}