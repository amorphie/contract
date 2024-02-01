using amorphie.contract.core.Entity.Contract;
using amorphie.contract.data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.data;


public class ContractDefinitionLanguageDetailConfiguration : ConfigurationBaseAudiEntity<ContractDefinitionLanguageDetail>,
        IEntityTypeConfiguration<ContractDefinitionLanguageDetail>

{
    public void Configure(EntityTypeBuilder<ContractDefinitionLanguageDetail> builder)
    {
        // builder.HasKey(x => new { x.LanguageId,x.Id});
        NavigationBuilderAutoInclude(builder, new List<string>
        {
            "MultiLanguage",
        });
    }
}
