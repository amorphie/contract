using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations
{
    public class CustomerConfiguration : ConfigurationBaseAudiEntity<Customer>
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