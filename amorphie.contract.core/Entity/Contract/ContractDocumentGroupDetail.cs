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
    public class ContractDocumentGroupDetail : AudiEntity
    {
        [Required]
 
        
        public Guid ContractDefinitionId { get; set; }

        // public ContractDefinition? ContractDefinition { get; set; }
        [Required]
        public Guid DocumentGroupId{ get; set; }
        public virtual DocumentGroup DocumentGroup { get; set; }
        [Required]
        public ushort AtLeastRequiredDocument { get; set; }
        public bool Required { get; set; }

    }
}