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
    public class DocumentOnlineSignConfiguration : ConfigurationBaseAuditEntity<DocumentOnlineSign>,
         IEntityTypeConfiguration<DocumentOnlineSign>

    {
        public void Configure(EntityTypeBuilder<DocumentOnlineSign> builder)
        {
            builder.Navigation(k => k.DocumentAllowedClientDetails).AutoInclude();
            builder.OwnsMany(product => product.Templates, builder => { builder.ToJson(); });

        }
    }
}