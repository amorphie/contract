using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Contract
{
    public class ContractTagConfiguration : ConfigurationBase<ContractTag>
    {
        public void Configure(EntityTypeBuilder<ContractTag> builder)
        {


        }
    }
}