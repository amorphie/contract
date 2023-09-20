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
    [Table("ContractDefinition", Schema = "Cont")]
    public class ContractDefinition : EntityBase
    {
        public string Code { get; set; }
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }
        public virtual ICollection<ContractDocumentDetail>? ContractDocumentDetails { get; set; }
        public virtual ICollection<ContractDocumentGroupDetail>? ContractDocumentGroupDetails { get; set; }
        public virtual ICollection<ContractTag>? ContractTags { get; set; }
        public virtual ICollection<ContractEntityProperty>? ContractEntityProperty { get; set; }
        public virtual ICollection<ContractValidation>? ContractValidations { get; set; }
        


    }
}