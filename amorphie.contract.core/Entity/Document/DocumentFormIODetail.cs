using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentFormIODetail", Schema = "Doc")]

    public class DocumentFormIODetail : EntityBase
    {
         public Guid DocumentDefinitionId { get; set; }

        public DocumentDefinition DocumentDefinition { get; set; }
        public Guid DocumentFormIOId { get; set; }

        public DocumentFormIO DocumentFormIO { get; set; }
    }
}