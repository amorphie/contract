using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class UserSignedContractConfiguration : ConfigurationBaseAuditEntity<UserSignedContract>,
     IEntityTypeConfiguration<UserSignedContract>
    {
        public override void Configure(EntityTypeBuilder<UserSignedContract> builder)
        {
            NavigationBuilderAutoInclude(builder, new List<string>
            {
                "UserSignedContractDetails",
            });

            builder.HasIndex(x => x.CustomerId);

            builder.Property(k => k.ContractCode).IsRequired().HasMaxLength(1000);

        }
    }
}