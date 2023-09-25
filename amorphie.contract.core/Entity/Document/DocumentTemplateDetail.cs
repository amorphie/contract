using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTemplateDetail", Schema = "Doc")]

    public class DocumentTemplateDetail : EntityBase
    {
        [Required]
         public Guid DocumentDefinitionId { get; set; }
        // public DocumentDefinition? DocumentDefinition { get; set; }
        [Required]
        public Guid DocumentTemplateId { get; set; }

        public DocumentTemplate? DocumentTemplate { get; set; }
    }
}