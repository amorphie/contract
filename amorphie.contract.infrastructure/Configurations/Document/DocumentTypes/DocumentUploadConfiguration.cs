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
    public class DocumentUploadConfiguration : ConfigurationBaseAuditEntity<DocumentUpload>,
         IEntityTypeConfiguration<DocumentUpload>

    {
        public void Configure(EntityTypeBuilder<DocumentUpload> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentFormatDetails",
                "DocumentAllowedClientDetail",

            });

        }
    }
}