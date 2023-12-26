using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data.Configurations.DocumentGroups
{
    public class DocumentGroupConfiguration : ConfigurationBase<DocumentGroup>,
         IEntityTypeConfiguration<DocumentGroup>

    {
        public void Configure(EntityTypeBuilder<DocumentGroup> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "DocumentGroupDetails",
                "DocumentGroupLanguageDetail",
                "Status",
            });
        }
    }
}