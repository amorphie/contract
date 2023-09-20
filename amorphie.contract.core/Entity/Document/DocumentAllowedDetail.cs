using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentAllowedDetail", Schema = "Doc")]
    public class DocumentAllowedDetail : EntityBase
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }

        // public DocumentDefinition? DocumentDefinition { get; set; }
        [Required]
        public Guid DocumentAllowedId { get; set; }


        public DocumentAllowed? DocumentAllowed { get; set; }
    }
}