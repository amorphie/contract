using amorphie.contract.core.Entity.Document;
using amorphie.contract.infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentTsizlConfiguration : ConfigurationBaseAuditEntity<DocumentTsizl>,
         IEntityTypeConfiguration<DocumentTsizl>

    {
        public void Configure(EntityTypeBuilder<DocumentTsizl> builder)
        {


        }
    }
}