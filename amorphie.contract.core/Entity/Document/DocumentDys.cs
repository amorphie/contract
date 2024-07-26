using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentDys", Schema = "Doc")]
    public class DocumentDys : AuditEntity
    {
        [Required]
        public int ReferenceId { get; set; }
        public int ReferenceKey { get; set; }
        [Required]
        public string ReferenceName { get; set; }
        [Required]
        public string Fields { get; set; }
        [Required]
        public string TitleFields { get; set; }
        public Guid DocumentDefinitionId { get; set; }
        [ForeignKey("DocumentDefinitionId")]
        public DocumentDefinition DocumentDefinition { get; set; } = default!;
    }

}