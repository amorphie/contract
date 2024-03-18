using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.DocumentGroups
{
    public class DocumentGroupLanguageDetailConfiguration : ConfigurationBaseAudiEntity<DocumentGroupLanguageDetail>,
         IEntityTypeConfiguration<DocumentGroupLanguageDetail>

    {
        public void Configure(EntityTypeBuilder<DocumentGroupLanguageDetail> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "MultiLanguage",
            });
        }
    }
}