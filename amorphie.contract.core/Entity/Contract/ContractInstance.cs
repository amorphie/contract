using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractInstance", Schema = "Cont")]
    public class ContractInstance : AudiEntity
    {
        public string ContractCode { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<ContractInstanceDetail> ContractInstanceDetails { get; set; } = null!;

    }
}