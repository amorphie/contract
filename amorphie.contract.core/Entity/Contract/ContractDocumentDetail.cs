using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractDocumentDetail", Schema = "Cont")]

    public class ContractDocumentDetail : AudiEntity
    {
        [Required]

        public Guid ContractDefinitionId { get; set; }
        [ForeignKey("ContractDefinitionId")]

        public ContractDefinition ContractDefinition { get; set; } = default!;
        [Required]

        public Guid DocumentDefinitionId { get; set; }
        [Required]
        [ForeignKey("DocumentDefinitionId")]

        public DocumentDefinition DocumentDefinition { get; set; } = default!;
        [Required]
        public EUseExisting UseExisting { get; set; }
        // [Required]
        // public string DocumentDefinitionSemver { get; set; }
        [Required]
        public bool Required { get; set; }
        [Required]
        public short Order { get; set; }

    }
}