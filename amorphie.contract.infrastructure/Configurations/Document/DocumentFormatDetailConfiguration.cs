using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentFormatDetailConfiguration : ConfigurationBaseAuditEntity<DocumentFormatDetail>,
         IEntityTypeConfiguration<DocumentFormatDetail>
    {
        public void Configure(EntityTypeBuilder<DocumentFormatDetail> builder)
        {
            builder.Navigation(k => k.DocumentFormat).AutoInclude();
        }
    }
}