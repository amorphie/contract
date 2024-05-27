using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractCategory", Schema = "Cont")]
    public class ContractCategory : BaseEntity
    {
        public List<ContractCategoryDetail> ContractCategoryDetails { get; set; } = new List<ContractCategoryDetail>();
        public Dictionary<string, string> Titles { get; set; } = default!;
    }
}

