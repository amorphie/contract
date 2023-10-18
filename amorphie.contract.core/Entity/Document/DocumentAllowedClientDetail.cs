using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentAllowedClientDetail", Schema = "Doc")]
    public class DocumentAllowedClientDetail : EntityBase
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        // public DocumentDefinition? DocumentDefinition { get; set; }
        [Required]
        public Guid DocumentAllowedClientId { get; set; }
        public DocumentAllowedClient? DocumentAllowedClients { get; set; }
    }
}