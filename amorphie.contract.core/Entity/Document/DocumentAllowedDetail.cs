using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentAllowedDetail", Schema = "Doc")]

    public class DocumentAllowedDetail : EntityBase
    {
        public Guid DocumentDefinitionId { get; set; }

        // public DocumentDefinition? DocumentDefinition { get; set; }
        public Guid DocumentAllowedId { get; set; }


        public DocumentAllowed? DocumentAllowed { get; set; }
    }
}