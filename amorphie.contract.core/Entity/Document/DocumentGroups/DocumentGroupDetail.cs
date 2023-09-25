using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document.DocumentGroups
{
    [Table("DocumentGroupDetail", Schema = "DocGroup")]

    public class DocumentGroupDetail : EntityBase
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        public DocumentDefinition DocumentDefinition { get; set; }
        [Required]
        public Guid DocumentGroupID { get; set; }
        public DocumentGroup DocumentGroup { get; set; }

    }
}