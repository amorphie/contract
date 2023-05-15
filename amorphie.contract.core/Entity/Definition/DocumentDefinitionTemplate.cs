using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity.Definition
{
    [Table("DocumentTemplate", Schema = "Definition")]

    public class DocumentDefinitionTemplate : BaseEntity
    {
        [Key]
        [MaxLength(36)]
        public Guid DocumentDefinitionId { get; set; }
        [Key]
        [MaxLength(36)]
        public Guid LanguageId { get; set; }
        public string Name { get; set; }

    }
}