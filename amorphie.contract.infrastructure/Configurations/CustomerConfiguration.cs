using amorphie.contract.core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations
{
    public class CustomerConfiguration : ConfigurationBaseAudiEntity<Customer>,
                 IEntityTypeConfiguration<Customer>

    {
        public CustomerConfiguration(EntityTypeBuilder<Customer> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentList",
            });

        }
    }
}