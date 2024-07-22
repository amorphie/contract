using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentMigrationAggregationConfiguration : IEntityTypeConfiguration<DocumentMigrationAggregation>
    {
        public void Configure(EntityTypeBuilder<DocumentMigrationAggregation> builder)
        {
            builder.Property(k => k.DocumentCode).HasMaxLength(250);
            builder.HasIndex(x => x.DocumentCode).IsUnique();
            builder.OwnsMany(d => d.ContractCodes, builder => { builder.ToJson(); });
        }
    }

}
