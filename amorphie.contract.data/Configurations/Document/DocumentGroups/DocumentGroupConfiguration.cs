using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations.DocumentGroups
{
     public class DocumentGroupConfiguration : ConfigurationBase<DocumentGroup>
    {
        public void Configure(EntityTypeBuilder<DocumentGroup> builder)
        {
             
        }
    }
}