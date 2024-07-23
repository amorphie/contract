using System.Text.Json;
using System.Text.Json.Serialization;
using amorphie.contract.core.Entity;
using amorphie.contract.core.Model.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations
{
    public class CustomerConfiguration : ConfigurationBaseAuditEntity<Customer>, IEntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration(EntityTypeBuilder<Customer> builder)
        {
            builder.Navigation(k => k.DocumentList).AutoInclude();

            builder.Property(k => k.Reference).HasMaxLength(11).HasColumnType("varchar(11)").IsRequired();
            builder.Property(k => k.Owner).HasMaxLength(50).HasColumnType("varchar(50)");;
            builder.HasIndex(x => x.Reference).IsUnique();
            builder.HasIndex(x => x.CustomerNo).IsUnique();
            builder.Property(k => k.TaxNo).HasMaxLength(10).HasColumnType("varchar(10)");;

        }
    }
}