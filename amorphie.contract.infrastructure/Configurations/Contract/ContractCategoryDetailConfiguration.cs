using System;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Contract
{
	public class ContractCategoryDetailConfiguration : ConfigurationBaseAuditEntity<ContractCategoryDetail>,
     IEntityTypeConfiguration<ContractCategoryDetail>
    {
        public virtual void Configure(EntityTypeBuilder<ContractCategoryDetail> builder)
        {
            builder.Navigation(k => k.ContractDefinition).AutoInclude();
        }
    }
}

