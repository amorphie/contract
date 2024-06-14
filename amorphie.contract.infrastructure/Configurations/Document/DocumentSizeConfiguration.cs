using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentSizeConfiguration : ConfigurationBaseAuditEntity<DocumentSize>,
             IEntityTypeConfiguration<DocumentSize>

    {
        public void Configure(EntityTypeBuilder<DocumentSize> builder)
        {
        }
    }
}