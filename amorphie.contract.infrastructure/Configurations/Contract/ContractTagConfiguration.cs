using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractTagConfiguration : ConfigurationBaseAuditEntity<ContractTag>,
     IEntityTypeConfiguration<ContractTag>

    {
        public void Configure(EntityTypeBuilder<ContractTag> builder)
        {

            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "Tags",
            });

        }
    }
}