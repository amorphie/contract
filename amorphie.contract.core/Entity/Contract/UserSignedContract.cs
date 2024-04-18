using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("UserSignedContract", Schema = "Cont")]
    public class UserSignedContract : AuditEntity
    {
        public string ContractCode { get; set; }
        public Guid ContractInstanceId { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<UserSignedContractDetail> UserSignedContractDetails { get; set; } = null!;

    }
}