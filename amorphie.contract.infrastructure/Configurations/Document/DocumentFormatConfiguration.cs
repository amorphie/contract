using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentFormatConfiguration : ConfigurationBaseAuditEntity<DocumentFormat>,
         IEntityTypeConfiguration<DocumentFormat>
    {
        public void Configure(EntityTypeBuilder<DocumentFormat> builder)
        {
            builder.Navigation(k => k.DocumentFormatType).AutoInclude();
            builder.Navigation(k => k.DocumentSize).AutoInclude();
        }
    }
}