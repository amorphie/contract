using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Definition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            // builder.HasKey(x => new { x.DocumentDefinition.Id});

        }
    }
}