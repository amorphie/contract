using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentTagsDetailConfiguration : ConfigurationBaseAuditEntity<DocumentTagsDetail>,
             IEntityTypeConfiguration<DocumentTagsDetail>
    {
        public void Configure(EntityTypeBuilder<DocumentTagsDetail> builder)
        {
            builder.Navigation(k => k.Tags).AutoInclude();
        }
    }
}