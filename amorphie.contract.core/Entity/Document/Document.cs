using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Entity.Document
{
    [Table("Document", Schema = "Doc")]
    public class Document : AudiEntity
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        [ForeignKey("DocumentDefinitionId")]

        public DocumentDefinition DocumentDefinition { get; set; } = default!;
        [Required]
        public Guid DocumentContentId { get; set; }
        [ForeignKey("DocumentContentId")]

        public DocumentContent DocumentContent { get; set; } = default!;

        public EStatus Status { get; set; } = default!;
        public ICollection<DocumentInstanceEntityProperty>? DocumentInstanceEntityPropertys { get; set; } = new List<DocumentInstanceEntityProperty>();
        public ICollection<DocumentInstanceNote>? DocumentInstanceNotes { get; set; } = new List<DocumentInstanceNote>();
        
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = default!;
    }
}