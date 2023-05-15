using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Definition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentDefinitionTemplateEntityConfiguration : IEntityTypeConfiguration<DocumentDefinitionTemplate>
    {
        public void Configure(EntityTypeBuilder<DocumentDefinitionTemplate> builder)
        {
            builder.HasKey(x => new { x.LanguageId,x.DocumentDefinitionId});

        }
    }
}