using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentFormatConfiguration : ConfigurationBaseAuditEntity<DocumentFormat>,
         IEntityTypeConfiguration<DocumentFormat>

    {
        public void Configure(EntityTypeBuilder<DocumentFormat> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentFormatType",
                "DocumentSize",
            });
        }
    }
}