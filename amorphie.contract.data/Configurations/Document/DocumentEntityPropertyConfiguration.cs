using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Definition
{
    public class DocumentEntityPropertyConfiguration : ConfigurationBaseAudiEntity<DocumentEntityProperty>
    {
        public void Configure(EntityTypeBuilder<DocumentEntityProperty> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "EntityProperty",
            });

        }
    }
}