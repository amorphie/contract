using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentFormatDetailConfiguration : ConfigurationBaseAudiEntity<DocumentFormatDetail>,
         IEntityTypeConfiguration<DocumentFormatDetail>

    {
        public void Configure(EntityTypeBuilder<DocumentFormatDetail> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentFormat",
            });
        }
    }
}