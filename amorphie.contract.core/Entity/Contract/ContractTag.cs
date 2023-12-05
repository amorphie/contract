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
    [Table("ContractTag", Schema = "Cont")]
    public class ContractTag : AudiEntity
    {
        [Required]
        public Guid ContractDefinitionId { get; set; }
        [Required]
        public Guid TagId { get; set; }
        public virtual Common.Tag Tags { get; set; }

    }
}