using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.EAV;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.EAV
{
    public class EntityPropertyConfiguration : ConfigurationBase<EntityProperty>,
                 IEntityTypeConfiguration<EntityProperty>

    {
        public void Configure(EntityTypeBuilder<EntityProperty> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "EntityPropertyValue",
            });

        }

    }
}