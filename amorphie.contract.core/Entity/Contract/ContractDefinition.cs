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
    [Table("ContractDefinition", Schema = "Cont")]
    [Index(nameof(Code), IsUnique = true)]
    public class ContractDefinition : BaseEntity
    {
        // [Required]
        // public Guid StatusId { get; set; }
        // [ForeignKey("StatusId")]

        public EStatus Status { get; set; } = default!;
        public EBankEntity BankEntity { get; set; } = default!;
        public ICollection<ContractDocumentDetail> ContractDocumentDetails { get; set; } = new List<ContractDocumentDetail>();
        public ICollection<ContractDocumentGroupDetail> ContractDocumentGroupDetails { get; set; } = new List<ContractDocumentGroupDetail>();
        public ICollection<ContractTag> ContractTags { get; set; } = new List<ContractTag>();
        public ICollection<ContractEntityProperty> ContractEntityProperty { get; set; } = new List<ContractEntityProperty>();
        public ICollection<ContractValidation> ContractValidations { get; set; } = new List<ContractValidation>();

    }
}