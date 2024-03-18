using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractEntityPropertyConfiguration : ConfigurationBaseAudiEntity<ContractEntityProperty>,
     IEntityTypeConfiguration<ContractEntityProperty>

    {
        public void Configure(EntityTypeBuilder<ContractEntityProperty> builder)
        {

            var list = new List<string>
            {
                "EntityProperty",
            };
            NavigationBuilderAutoInclude(builder, list);
        }
    }
}