using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.EAV;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentEntityProperty", Schema = "Doc")]
    public class DocumentEntityProperty : AudiEntity
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        [ForeignKey("DocumentDefinitionId")]

        public DocumentDefinition DocumentDefinition { get; set; } = default!;
        [Required]

        public Guid EntityPropertyId { get; set; }
        [ForeignKey("EntityPropertyId")]

        public   EntityProperty EntityProperty { get; set; } = default!;

    }
}