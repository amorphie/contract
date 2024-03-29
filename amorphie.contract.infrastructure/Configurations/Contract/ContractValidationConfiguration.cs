using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractValidationConfiguration : ConfigurationBaseAudiEntity<ContractValidation>,
     IEntityTypeConfiguration<ContractValidation>

    {
        public void Configure(EntityTypeBuilder<ContractValidation> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "Validations",
            });

        }
    }
}