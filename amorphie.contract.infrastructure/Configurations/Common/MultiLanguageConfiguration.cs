using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Common
{
    public class MultiLanguageConfiguration : ConfigurationBaseAuditEntity<MultiLanguage>,
    IEntityTypeConfiguration<MultiLanguage>

    {
        public void Configure(EntityTypeBuilder<MultiLanguage> builder)
        {
            var list = new List<string>
            {
                "LanguageType",
            };
            NavigationBuilderAutoInclude(builder, list);
        }
    }
}