using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentConfiguration : ConfigurationBaseAuditEntity<core.Entity.Document.Document>,
         IEntityTypeConfiguration<core.Entity.Document.Document>

    {
        public void Configure(EntityTypeBuilder<core.Entity.Document.Document> builder)
        {
            builder.Navigation(k => k.DocumentDefinition).AutoInclude();
            builder.Navigation(k => k.DocumentInstanceNotes).AutoInclude();
            builder.Navigation(k => k.DocumentContent).AutoInclude();
            builder.Navigation(k => k.Customer).AutoInclude();
            builder.OwnsMany(d => d.InstanceMetadata, builder => { builder.ToJson(); });
        }
    }
}