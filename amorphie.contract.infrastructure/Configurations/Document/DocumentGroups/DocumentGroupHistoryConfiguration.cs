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
    public class DocumentGroupHistoryConfiguration : ConfigurationBaseAudiEntity<DocumentGroupHistory>,
     IEntityTypeConfiguration<DocumentGroupHistory>
    {
        public virtual void Configure(EntityTypeBuilder<DocumentGroupHistory> builder)
        {
            builder.Property(x => x.History)
            .HasColumnType("jsonb");
        }
    }
}