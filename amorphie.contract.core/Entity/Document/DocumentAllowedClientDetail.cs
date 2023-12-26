using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Entity.Document.DocumentTypes;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentAllowedClientDetail", Schema = "Doc")]
    public class DocumentAllowedClientDetail : AudiEntity
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        [Required]
        [ForeignKey("DocumentDefinitionId")]

        public DocumentDefinition DocumentDefinition { get; set; } = default!;
        [Required]
        public Guid DocumentAllowedClientId { get; set; }
        [ForeignKey("DocumentAllowedClientId")]

        public DocumentAllowedClient DocumentAllowedClients { get; set; } = default!;
    }
}