using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractDocumentGroupDetailConfiguration : ConfigurationBaseAuditEntity<ContractDocumentGroupDetail>,
     IEntityTypeConfiguration<ContractDocumentGroupDetail>

    {
        public void Configure(EntityTypeBuilder<ContractDocumentGroupDetail> builder)
        {
            var list = new List<string>
            {
                "DocumentGroup",
            };
            NavigationBuilderAutoInclude(builder, list);

        }
    }
}