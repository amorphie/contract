using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.EAV;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations.EAV
{
    public class EntityPropertyValueConfiguration : ConfigurationBase<EntityPropertyValue>
    {
        public void Configure(EntityTypeBuilder<EntityPropertyValue> builder)
        {


        }

    }
}