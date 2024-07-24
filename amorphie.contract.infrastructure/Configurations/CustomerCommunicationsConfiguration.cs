using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using amorphie.contract.core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations
{
    public class CustomerCommunicationsConfiguration : ConfigurationBaseAuditEntity<CustomerCommunication>, IEntityTypeConfiguration<CustomerCommunication>
    {
        public virtual void Configure(EntityTypeBuilder<CustomerCommunication> builder)
        {
            builder.Property(k => k.DocumentList).HasColumnType("jsonb").HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions(JsonSerializerDefaults.General)),
                    v => JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions(JsonSerializerDefaults.General))!);
        }
    }
}