using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("UserSignedContractDetail", Schema = "Cont")]
    public class UserSignedContractDetail : AuditEntity
    {
        public Guid DocumentInstanceId { get; set; }

        public Guid UserSignedContractId { get; set; }

        [ForeignKey("UserSignedContractId")]
        public UserSignedContract UserSignedContract { get; set; }
    }
}