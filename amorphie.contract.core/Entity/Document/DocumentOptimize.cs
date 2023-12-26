using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentOptimize", Schema = "Doc")]
    public class DocumentOptimize : AudiEntity
    {
        public bool Size { get; set; }
        [Required]
        public Guid DocumentOptimizeTypeId { get; set; }
        [ForeignKey("DocumentOptimizeTypeId")]

        public DocumentOptimizeType DocumentOptimizeType { get; set; } = default!;
    }
}