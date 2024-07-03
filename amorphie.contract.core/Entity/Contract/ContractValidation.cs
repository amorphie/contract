using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractValidation", Schema = "Cont")]
    public class ContractValidation : AuditEntity
    {
        [Required]
        public Guid ContractDefinitionId { get; set; }
        [ForeignKey("ContractDefinitionId")]
        public ContractDefinition ContractDefinition { get; set; } = default!;
        [Required]
        public Guid ValidationId { get; set; }
        [ForeignKey("ValidationId")]

        public Common.Validation Validations { get; set; } = default!;

    }
}