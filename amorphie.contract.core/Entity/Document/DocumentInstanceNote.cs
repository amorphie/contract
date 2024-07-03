using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentInstanceNote", Schema = "Doc")]
    public class DocumentInstanceNote : AuditEntity
    {
        [Required]
        public Guid DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; } = default!;
        [Required]
        public string Note { get; set; }
    }
}