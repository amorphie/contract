using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentMigrationDysDocumentConfiguration : IEntityTypeConfiguration<DocumentMigrationDysDocument>
    {
        public void Configure(EntityTypeBuilder<DocumentMigrationDysDocument> builder)
        {
            builder.Property(k => k.Title).HasMaxLength(350);
            builder.Property(k => k.Notes).HasMaxLength(500);
            builder.Property(k => k.OwnerId).HasMaxLength(250);
            builder.Property(k => k.Channel).HasMaxLength(250);

            builder.HasIndex(x => x.DocId).IsUnique();
        }
    }

}
