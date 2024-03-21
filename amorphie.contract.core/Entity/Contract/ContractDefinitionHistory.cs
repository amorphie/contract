using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractDefinitionHistory", Schema = "Cont")]
    public class ContractDefinitionHistory : AudiEntity
    {
        [Required]
        public ContractDefinitionHistoryModel ContractDefinitionHistoryModel { get; set; }
        public Guid ContractDefinitionId { get; set; }
        [ForeignKey("ContractDefinitionId")]
        public ContractDefinition ContractDefinition { get; set; } = default!;
    }

    public class ContractDefinitionHistoryModel : BaseEntity
    {
        public EStatus Status { get; set; } = default!;
        public EBankEntity BankEntity { get; set; } = default!;
        public ICollection<ContractDefinitionLanguageDetail>? ContractDefinitionLanguageDetails { get; set; } = new List<ContractDefinitionLanguageDetail>();
        public ICollection<ContractDocumentDetail> ContractDocumentDetails { get; set; } = new List<ContractDocumentDetail>();
        public ICollection<ContractDocumentGroupDetail> ContractDocumentGroupDetails { get; set; } = new List<ContractDocumentGroupDetail>();
        public ICollection<ContractTag> ContractTags { get; set; } = new List<ContractTag>();
        public ICollection<ContractEntityProperty> ContractEntityProperty { get; set; } = new List<ContractEntityProperty>();
        public ICollection<ContractValidation> ContractValidations { get; set; } = new List<ContractValidation>();
    }
}