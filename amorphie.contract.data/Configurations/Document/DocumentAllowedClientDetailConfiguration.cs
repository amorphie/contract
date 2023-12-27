using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentAllowedClientDetailConfiguration : ConfigurationBaseAudiEntity<DocumentAllowedClientDetail>,
         IEntityTypeConfiguration<DocumentAllowedClientDetail>

    {
        public void Configure(EntityTypeBuilder<DocumentAllowedClientDetail> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentAllowedClients",
            });

        }
    }
}