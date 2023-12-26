using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document.DocumentGroups
{
    [Table("DocumentGroupDetail", Schema = "DocGroup")]

    public class DocumentGroupDetail : AudiEntity
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        [ForeignKey("DocumentDefinitionId")]

        public DocumentDefinition DocumentDefinition { get; set; } = default!;
        [Required]
        public Guid DocumentGroupId { get; set; }
        [ForeignKey("DocumentGroupId")]

        public DocumentGroup DocumentGroup { get; set; } = default!;

    }
}