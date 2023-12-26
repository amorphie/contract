using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentOperationsTagsDetailConfiguration : ConfigurationBaseAudiEntity<DocumentOperationsTagsDetail>
    {
        public void Configure(EntityTypeBuilder<DocumentOperationsTagsDetail> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentOperations",
                "Tags",
            });

        }
    }
}