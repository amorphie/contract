using System.Text.Json;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Contract
{
	public class ContractCategoryConfiguration : ConfigurationBase<ContractCategory>,
     IEntityTypeConfiguration<ContractCategory>
    {
        public virtual void Configure(EntityTypeBuilder<ContractCategory> builder)
        {
            builder.Navigation(k => k.ContractCategoryDetails).AutoInclude();

            builder.Property(k => k.Titles).HasColumnType("jsonb").HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions(JsonSerializerDefaults.General)),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions(JsonSerializerDefaults.General))!);
        }
    }
}

