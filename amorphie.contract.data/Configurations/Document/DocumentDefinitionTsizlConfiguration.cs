using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentDenfinitionTsizlConfiguration : ConfigurationBaseAudiEntity<DocumentDefinitionTsizl>,
         IEntityTypeConfiguration<DocumentDefinitionTsizl>

    {
        public void Configure(EntityTypeBuilder<DocumentDefinitionTsizl> builder)
        {


        }
    }
}