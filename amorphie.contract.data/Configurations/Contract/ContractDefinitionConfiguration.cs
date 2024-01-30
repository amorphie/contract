using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Contract
{
    public class ContractDefinitionConfiguration : ConfigurationBase<ContractDefinition>,
     IEntityTypeConfiguration<ContractDefinition>
    {
        public virtual void Configure(EntityTypeBuilder<ContractDefinition> builder)
        {
            var list = new List<string>
            {
                "ContractDocumentDetails",
                "ContractDocumentGroupDetails",
                "ContractTag",
                "ContractEntityProperty",
                "ContractValidations",
            };
            NavigationBuilderAutoInclude(builder, list);
      
        }
    }
}