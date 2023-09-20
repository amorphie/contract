using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractValidation", Schema = "Cont")]
    public class ContractValidation : EntityBase
    {
        public Guid ContractDefinitionId { get; set; }

        public Guid ValidationId { get; set; }
        public Common.Validation Validations { get; set; }

    }
}