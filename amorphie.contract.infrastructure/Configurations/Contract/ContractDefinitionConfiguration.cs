using System.Text.Json;
using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class ContractDefinitionConfiguration : ConfigurationBase<ContractDefinition>,
     IEntityTypeConfiguration<ContractDefinition>
    {
        public virtual void Configure(EntityTypeBuilder<ContractDefinition> builder)
        {
            var list = new List<string>
            {
                "ContractDocumentDetails",
                "ContractDocumentGroupDetails",
                "ContractTag",
                "ContractValidations"
            };
            NavigationBuilderAutoInclude(builder, list);
            //
            builder.HasIndex(x => new
            {
                x.Code,
                x.BankEntity
            }).IsUnique();

            builder.Property(k => k.Titles).HasColumnType("jsonb").HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions(JsonSerializerDefaults.General)),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions(JsonSerializerDefaults.General))!);

            builder.OwnsMany(d => d.DefinitionMetadata, builder => { builder.ToJson(); });

        }
    }
}