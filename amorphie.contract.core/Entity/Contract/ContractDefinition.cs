using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractDefinition", Schema = "Cont")]
    public class ContractDefinition : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<DocumentDefinition> DocumentDefinitions { get; set; }
        public virtual ICollection<ContractEntityProperty> ContractEntityPropertys { get; set; }

    }
}