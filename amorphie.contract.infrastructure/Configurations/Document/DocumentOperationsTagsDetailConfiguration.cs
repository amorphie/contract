using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentOperationsTagsDetailConfiguration : ConfigurationBaseAuditEntity<DocumentOperationsTagsDetail>,
             IEntityTypeConfiguration<DocumentOperationsTagsDetail>
    {
        public void Configure(EntityTypeBuilder<DocumentOperationsTagsDetail> builder)
        {
            builder.Navigation(k => k.DocumentOperations).AutoInclude();
            builder.Navigation(k => k.Tags).AutoInclude();
        }
    }
}