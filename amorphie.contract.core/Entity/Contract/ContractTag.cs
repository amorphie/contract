using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractTag", Schema = "Cont")]
    public class ContractTag : EntityBase
    {
        public Guid ContractDefinitionId { get; set; }

        // public DocumentDefinition? DocumentDefinition { get; set; }
        public string Code { get; set; }
        public string Contact { get; set; }

    }
}