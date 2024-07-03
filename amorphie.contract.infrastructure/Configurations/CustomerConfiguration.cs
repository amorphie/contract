using amorphie.contract.core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations
{
    public class CustomerConfiguration : ConfigurationBaseAuditEntity<Customer>,IEntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration(EntityTypeBuilder<Customer> builder)
        {
            builder.Navigation(k => k.DocumentList).AutoInclude();
        }
    }
}