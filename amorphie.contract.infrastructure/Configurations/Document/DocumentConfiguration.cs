using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentConfiguration : ConfigurationBaseAudiEntity<core.Entity.Document.Document>,
         IEntityTypeConfiguration<core.Entity.Document.Document>

    {
        public void Configure(EntityTypeBuilder<core.Entity.Document.Document> builder)
        {

            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentDefinition",
                "DocumentInstanceNotes",
                "DocumentContent",
                "Status",
                "Customer",
            });

            builder.OwnsMany(d => d.InstanceMetadata, builder => { builder.ToJson(); });
        }
    }
}