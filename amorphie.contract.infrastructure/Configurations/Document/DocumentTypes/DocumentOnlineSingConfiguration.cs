using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Document.DocumentTypes
{
    public class DocumentOnlineSingConfiguration : ConfigurationBaseAudiEntity<DocumentOnlineSing>,
         IEntityTypeConfiguration<DocumentOnlineSing>

    {
        public void Configure(EntityTypeBuilder<DocumentOnlineSing> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentAllowedClientDetails",
                "DocumentTemplateDetails",

            });
            builder.OwnsMany(product => product.Templates, builder => { builder.ToJson(); });

        }
    }
}