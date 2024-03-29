using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractDocumentDetailConfiguration : ConfigurationBaseAudiEntity<ContractDocumentDetail>,
     IEntityTypeConfiguration<ContractDocumentDetail>

    {
        public void Configure(EntityTypeBuilder<ContractDocumentDetail> builder)
        {
            var list = new List<string>
            {
                "DocumentDefinition",
            };
            NavigationBuilderAutoInclude(builder, list);

            //
        }
    }
}