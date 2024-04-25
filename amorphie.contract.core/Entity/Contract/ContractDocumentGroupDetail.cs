using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractDocumentGroupDetail", Schema = "Cont")]
    public class ContractDocumentGroupDetail : AuditEntity
    {
        [Required]
        public Guid ContractDefinitionId { get; set; }
        [ForeignKey("ContractDefinitionId")]

        public ContractDefinition ContractDefinition { get; set; } = default!;
        [Required]
        public Guid DocumentGroupId { get; set; }
        [ForeignKey("DocumentGroupId")]

        public DocumentGroup DocumentGroup { get; set; } = default!;
        [Required]
        public ushort AtLeastRequiredDocument { get; set; }
        public bool Required { get; set; }

    }
}