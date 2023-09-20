using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractDocumentGroupDetail", Schema = "Cont")]
    public class ContractDocumentGroupDetail : EntityBase
    {
        public Guid ContractDefinitionId { get; set; }

        // public ContractDefinition? ContractDefinition { get; set; }
        public Guid DocumentGroupId { get; set; }

        public DocumentGroup? DocumentGroup { get; set; }
        public  string AtLeastRequiredDocument{ get; set; }
        public bool Required{ get; set; }

    }
}