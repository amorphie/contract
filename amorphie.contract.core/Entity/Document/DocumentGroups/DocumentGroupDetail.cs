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
        // [ForeignKey(nameof(DocumentDefinitionCode))]
        public string DocumentDefinitionCode { get; set; }
        // public virtual DocumentDefinition DocumentDefinition { get; set; }
        public string MinVersion { get; set; }
        [Required]
        public Guid DocumentGroupId { get; set; }
        // public DocumentGroup DocumentGroup { get; set; }

    }
}