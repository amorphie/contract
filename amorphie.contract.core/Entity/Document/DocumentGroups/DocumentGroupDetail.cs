using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace amorphie.contract.core.Entity.Document.DocumentGroups
{
    [Table("DocumentGroupDetail", Schema = "DocGroup")]

    public class DocumentGroupDetail : AuditEntity
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        [ForeignKey("DocumentDefinitionId")]

        public DocumentDefinition DocumentDefinition { get; set; } = default!;
        [Required]
        public Guid DocumentGroupId { get; set; }
        [ForeignKey("DocumentGroupId")]

        public DocumentGroup DocumentGroup { get; set; } = default!;
        public bool SendMail { get; set; }

    }
}