using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity.Definition
{
    [Table("DocumentType", Schema = "Definition")]
    public class DocumentDefinitionType : BaseEntity
    {
        [Key]
        [MaxLength(36)]
        public Guid DocumentDefinitionId { get; set; }

        public string Name { get; set; }
        public string ContentType { get; set; }


    }
}