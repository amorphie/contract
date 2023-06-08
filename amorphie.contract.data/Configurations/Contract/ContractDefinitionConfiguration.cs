using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Contract
{
    public class ContractDefinitionConfiguration : IEntityTypeConfiguration<ContractDefinition>
    {
        public void Configure(EntityTypeBuilder<ContractDefinition> builder)
        {

           
        }
    }
}