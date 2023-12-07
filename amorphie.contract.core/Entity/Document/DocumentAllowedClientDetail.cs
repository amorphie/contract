using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentAllowedClientDetail", Schema = "Doc")]
    public class DocumentAllowedClientDetail : AudiEntity
    {
        [Required]
        
         
        public Guid DocumentDefinitionId { get; set; }
        // public DocumentDefinition? DocumentDefinition { get; set; }
        [Required]
        public Guid DocumentAllowedClientId { get; set; }
        public virtual DocumentAllowedClient? DocumentAllowedClients { get; set; }
    }
}