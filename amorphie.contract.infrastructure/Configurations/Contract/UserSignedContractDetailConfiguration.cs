using amorphie.contract.core.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace amorphie.contract.infrastructure.Configurations.Contract
{
    public class UserSignedContractDetailConfiguration : ConfigurationBaseAuditEntity<UserSignedContractDetail>,
 IEntityTypeConfiguration<UserSignedContractDetail>
    {
        public override void Configure(EntityTypeBuilder<UserSignedContractDetail> builder)
        {
            builder.HasIndex(x => x.DocumentInstanceId);

            builder.Property(k => k.DocumentInstanceId).IsRequired();

            builder.Property(k => k.UserSignedContractId).IsRequired();

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasIndex(x => new { x.DocumentInstanceId, x.UserSignedContractId }).IsUnique();
        }
    }
}