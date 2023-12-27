using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentOptimizeConfiguration : ConfigurationBaseAudiEntity<DocumentOptimize>,
             IEntityTypeConfiguration<DocumentOptimize>

    {
        public void Configure(EntityTypeBuilder<DocumentOptimize> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentOptimizeType",
            });
        }
    }
}