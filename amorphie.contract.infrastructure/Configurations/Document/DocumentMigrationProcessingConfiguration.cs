using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentMigrationProcessingConfiguration : IEntityTypeConfiguration<DocumentMigrationProcessing>
    {
        public void Configure(EntityTypeBuilder<DocumentMigrationProcessing> builder)
        {
            builder.HasIndex(x => x.TagId);
            builder.HasIndex(x => x.DocId);
            
            builder.Property(k => k.TagId).HasMaxLength(350);
            builder.Property(k => k.Status).HasMaxLength(50);
            builder.Property(k => k.ErrorMessage).HasMaxLength(500);
        }
    }

}
