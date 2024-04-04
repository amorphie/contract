using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractInstanceDetail", Schema = "Cont")]
    public class ContractInstanceDetail : AudiEntity
    {
        public Guid DocumentInstanceId { get; set; }

        public Guid ContractInstanceId { get; set; }

        [ForeignKey("ContractInstanceId")]
        public ContractInstance ContractInstance { get; set; }
    }
}