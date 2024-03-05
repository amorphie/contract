using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentTsizlConfiguration : ConfigurationBaseAudiEntity<DocumentTsizl>,
         IEntityTypeConfiguration<DocumentTsizl>

    {
        public void Configure(EntityTypeBuilder<DocumentTsizl> builder)
        {


        }
    }
}