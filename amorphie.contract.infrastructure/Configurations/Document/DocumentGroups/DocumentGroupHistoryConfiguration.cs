using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class DocumentGroupHistoryConfiguration : ConfigurationBaseAudiEntity<DocumentGroupHistory>,
     IEntityTypeConfiguration<DocumentGroupHistory>
    {
        public virtual void Configure(EntityTypeBuilder<DocumentGroupHistory> builder)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            builder.Property(e => e.DocumentGroupHistoryModel)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, settings),
                    v => JsonConvert.DeserializeObject<DocumentGroupHistoryModel>(v) 
                );
        }
    }
}