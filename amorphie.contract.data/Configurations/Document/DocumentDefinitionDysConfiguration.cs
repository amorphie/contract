using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentDenfinitionDysConfiguration : ConfigurationBaseAudiEntity<DocumentDefinitionDys>,
         IEntityTypeConfiguration<DocumentDefinitionDys>

    {
        public void Configure(EntityTypeBuilder<DocumentDefinitionDys> builder)
        {


        }
    }
}