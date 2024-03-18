using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Definition
{
    public class DocumentOperationsConfiguration : ConfigurationBaseAudiEntity<DocumentOperations>,
             IEntityTypeConfiguration<DocumentOperations>

    {
        public void Configure(EntityTypeBuilder<DocumentOperations> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentOperationsTagsDetail",
            });

        }
    }
}