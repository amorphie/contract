using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Common
{
    public class ValidationConfiguration : ConfigurationBaseAudiEntity<Validation>
    {
        public void Configure(EntityTypeBuilder<Validation> builder)
        {
            var list = new List<string>
            {
                "ValidationDecision",
            };
            NavigationBuilderAutoInclude(builder, list);
        }
    }
}