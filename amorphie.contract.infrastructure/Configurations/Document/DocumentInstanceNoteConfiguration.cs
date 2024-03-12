using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentInstanceNoteConfiguration : ConfigurationBaseAudiEntity<DocumentInstanceNote>,
         IEntityTypeConfiguration<DocumentInstanceNote>

    {
        public void Configure(EntityTypeBuilder<DocumentInstanceNote> builder)
        {


        }
    }
}