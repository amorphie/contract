using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentAllowedClientDetailConfiguration : ConfigurationBaseAuditEntity<DocumentAllowedClientDetail>,
         IEntityTypeConfiguration<DocumentAllowedClientDetail>
    {
        public void Configure(EntityTypeBuilder<DocumentAllowedClientDetail> builder)
        {
            builder.Navigation(k => k.DocumentAllowedClients).AutoInclude();
        }
    }
}