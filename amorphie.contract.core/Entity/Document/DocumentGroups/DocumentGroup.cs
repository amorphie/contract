using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Entity.Document.DocumentGroups
{
    [Table("DocumentGroup", Schema = "DocGroup")]
    public class DocumentGroup : BaseEntity
    {

        // [Required]
        // public Guid StatusId { get; set; }
        // [ForeignKey("StatusId")]
        public EStatus Status { get; set; } = default!;
        public ICollection<DocumentGroupDetail> DocumentGroupDetails { get; set; } = new List<DocumentGroupDetail>();
        public ICollection<DocumentGroupLanguageDetail> DocumentGroupLanguageDetail { get; set; } = new List<DocumentGroupLanguageDetail>();
        public Dictionary<string, string> Titles { get; set; } = default!;

    }
}