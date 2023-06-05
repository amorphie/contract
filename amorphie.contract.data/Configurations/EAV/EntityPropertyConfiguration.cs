using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.EAV;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations.EAV
{
    public class EntityPropertyConfiguration: IEntityTypeConfiguration<EntityProperty>
    {
        public void Configure(EntityTypeBuilder<EntityProperty> builder)
        {

           
        }
        
    }
}