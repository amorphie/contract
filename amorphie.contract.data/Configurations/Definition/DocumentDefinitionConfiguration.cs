using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Definition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentDefinitionConfiguration : IEntityTypeConfiguration<DocumentDefinition>
    {
        public void Configure(EntityTypeBuilder<DocumentDefinition> builder)
        {
            // builder.HasKey(x => new { x.Id,x.Document.Id});
        }
    }
}