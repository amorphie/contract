using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Model.History;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractDefinitionHistory", Schema = "Cont")]
    public class ContractDefinitionHistory : AudiEntity
    {
        [Required]
        public ContractDefinitionHistoryModel ContractDefinitionHistoryModel { get; set; }
        public Guid ContractDefinitionId { get; set; }
    }

}