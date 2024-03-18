using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractDefinitionConfiguration : ConfigurationBase<ContractDefinition>,
     IEntityTypeConfiguration<ContractDefinition>
    {
        public virtual void Configure(EntityTypeBuilder<ContractDefinition> builder)
        {
            var list = new List<string>
            {
                "ContractDefinitionLanguageDetails",
                "ContractDocumentDetails",
                "ContractDocumentGroupDetails",
                "ContractTag",
                "ContractEntityProperty",
                "ContractValidations",
                "ContractDefinitionHistories"
            };
            NavigationBuilderAutoInclude(builder, list);
            //
            builder.HasIndex(x => new
            {
                x.Code,
                x.BankEntity
            }).IsUnique();
        }
    }
}