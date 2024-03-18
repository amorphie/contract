using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractDefinitionHistoryConfiguration : ConfigurationBaseAudiEntity<ContractDefinitionHistory>,
     IEntityTypeConfiguration<ContractDefinitionHistory>
    {
        public virtual void Configure(EntityTypeBuilder<ContractDefinitionHistory> builder)
        {
            builder.Property(x => x.History)
            .HasColumnType("jsonb");
        }
    }
}