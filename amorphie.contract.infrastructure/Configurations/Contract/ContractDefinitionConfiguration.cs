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

            builder.Navigation(k => k.ContractDocumentDetails).AutoInclude();
            builder.Navigation(k => k.ContractCategoryDetails).AutoInclude();
            builder.Navigation(k => k.ContractDocumentGroupDetails).AutoInclude();
            builder.Navigation(k => k.ContractTags).AutoInclude();
            builder.Navigation(k => k.ContractValidations).AutoInclude();
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