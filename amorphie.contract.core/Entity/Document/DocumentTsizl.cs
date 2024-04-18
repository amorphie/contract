using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Entity.Document;


namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTsizl", Schema = "Doc")]
    public class DocumentTsizl : AuditEntity
    {
        [Required]
        public string EngagementKind { get; set; }
        public Guid DocumentDefinitionId { get; set; }
        [ForeignKey("DocumentDefinitionId")]
        public DocumentDefinition DocumentDefinition { get; set; } = default!;
    }
}