using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractDefinition", Schema = "Cont")]
    public class ContractDefinition : BaseEntity
    {
        public EStatus Status { get; set; } = default!;
        public EBankEntity BankEntity { get; set; } = default!;
        public ICollection<ContractDocumentDetail> ContractDocumentDetails { get; set; } = new List<ContractDocumentDetail>();
        public ICollection<ContractDocumentGroupDetail> ContractDocumentGroupDetails { get; set; } = new List<ContractDocumentGroupDetail>();
        public ICollection<ContractTag> ContractTags { get; set; } = new List<ContractTag>();
        public ICollection<ContractValidation> ContractValidations { get; set; } = new List<ContractValidation>();
        public Dictionary<string, string> Titles { get; set; } = default!;
        public List<Metadata> DefinitionMetadata { get; set; } = new();

    }
}