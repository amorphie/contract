using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.data.Configurations.Contract
{
    public class ContractDocumentDetailConfiguration : IEntityTypeConfiguration<ContractDocumentDetail>
    {
        public void Configure(EntityTypeBuilder<ContractDocumentDetail> builder)
        {

           
        }
    }
}