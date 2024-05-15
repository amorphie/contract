using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Entity.Document.DocumentGroups
{
    [Table("DocumentGroup", Schema = "DocGroup")]
    public class DocumentGroup : BaseEntity
    {
        public ICollection<DocumentGroupDetail> DocumentGroupDetails { get; set; } = new List<DocumentGroupDetail>();
        public Dictionary<string, string> Titles { get; set; } = default!;

    }
}