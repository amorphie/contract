using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.EAV;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractEntityProperty", Schema = "Cont")]
    public class ContractEntityProperty : AudiEntity
    {
        [Required]
        public Guid ContractDefinitionId { get; set; }
        [ForeignKey("ContractDefinitionId")]
        public ContractDefinition ContractDefinition { get; set; } = default!;
        [Required]
        public Guid EntityPropertyId { get; set; }
        [ForeignKey("EntityPropertyId")]

        public EntityProperty EntityProperty { get; set; } = default!;

    }
}