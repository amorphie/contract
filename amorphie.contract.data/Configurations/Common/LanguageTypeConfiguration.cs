using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Common
{
    public class  LanguageTypeConfiguration : ConfigurationBase<LanguageType>
    {
        public void Configure(EntityTypeBuilder<LanguageType> builder)
        {
            
        }
    }
}