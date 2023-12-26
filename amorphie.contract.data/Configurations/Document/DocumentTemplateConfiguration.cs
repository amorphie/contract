using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentTemplateConfiguration : ConfigurationBaseAudiEntity<DocumentTemplate>,
             IEntityTypeConfiguration<DocumentTemplate>

    {
        public void Configure(EntityTypeBuilder<DocumentTemplate> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "LanguageType",
            });
        }
    }
}