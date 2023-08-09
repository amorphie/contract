using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations.Definition
{
     public class DocumentDefinitionGroupConfiguration : ConfigurationBase<DocumentDefinitionGroup>
    {
        public void Configure(EntityTypeBuilder<DocumentDefinitionGroup> builder)
        {
             
        }
    }
}