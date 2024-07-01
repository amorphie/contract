using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentOperationsConfiguration : ConfigurationBaseAuditEntity<DocumentOperations>,
             IEntityTypeConfiguration<DocumentOperations>
    {
        public void Configure(EntityTypeBuilder<DocumentOperations> builder)
        {
            builder.Navigation(k => k.DocumentOperationsTagsDetail).AutoInclude();
        }
    }
}