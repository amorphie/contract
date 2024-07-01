using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentFormatTypeConfiguration : ConfigurationBase<DocumentFormatType>,
             IEntityTypeConfiguration<DocumentFormatType>
    {
        public void Configure(EntityTypeBuilder<DocumentFormatType> builder)
        {
        }
    }
}