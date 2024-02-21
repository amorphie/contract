using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.EAV;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentInstanceEntityProperty", Schema = "Doc")]
    public class DocumentInstanceEntityProperty : AudiEntity
    {
        [Required]
        public Guid DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; } = default!;
        [Required]

        public Guid EntityPropertyId { get; set; }
        [ForeignKey("EntityPropertyId")]

        public EntityProperty EntityProperty { get; set; } = default!;

    }
}