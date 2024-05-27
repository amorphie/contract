using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractCategoryDetail", Schema = "Cont")]
    public class ContractCategoryDetail : AuditEntity
    {
        [Required]
        public Guid ContractDefinitionId { get; set; }
        [Required]
        public Guid ContractCategoryId { get; set; }

        [ForeignKey("ContractDefinitionId")]
        public ContractDefinition ContractDefinition { get; set; }
        [ForeignKey("ContractCategoryId")]
        public ContractCategory ContractCategory { get; set; }
    }
}

