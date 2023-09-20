using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractDocumentDetail", Schema = "Cont")]
    public class ContractDocumentDetail : EntityBase
    {
        public Guid ContractDefinitionId { get; set; }

        // public ContractDefinition? ContractDefinition { get; set; }
        public Guid DocumentDefinitionId { get; set; }

        public DocumentDefinition? DocumentDefinition { get; set; }

        public Guid UseExistingId { get; set; }
        public UseExisting? UseExisting { get; set; }
        public Guid VersionsId{ get; set; }
        public Versions? Versions{ get; set; }
        public bool Required{ get; set; }

    }
}