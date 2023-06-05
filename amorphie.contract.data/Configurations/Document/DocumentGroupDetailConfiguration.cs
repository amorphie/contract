using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations.Definition
{
     public class DocumentGroupDetailConfiguration : IEntityTypeConfiguration<DocumentDefinitionGroupDetail>
    {
        public void Configure(EntityTypeBuilder<DocumentDefinitionGroupDetail> builder)
        {
            // builder.HasKey(x => new { x.LanguageId,x.Id});
        }
    }
}