using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractConfiguration : ConfigurationBaseAuditEntity<core.Entity.Contract.Contract>,
     IEntityTypeConfiguration<core.Entity.Contract.Contract>

    {
        public void Configure(EntityTypeBuilder<core.Entity.Contract.Contract> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "Validations",
            });

        }
    }
}