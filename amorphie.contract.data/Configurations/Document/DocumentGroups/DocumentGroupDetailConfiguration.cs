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
     public class DocumentGroupDetailConfiguration : ConfigurationBaseAudiEntity<DocumentGroupDetail>
    {
        public void Configure(EntityTypeBuilder<DocumentGroupDetail> builder)
        {
            // builder.HasKey(x => new { x.LanguageId,x.Id});
        }
    }
}