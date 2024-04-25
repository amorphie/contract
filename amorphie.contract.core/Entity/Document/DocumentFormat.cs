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
    [Table("DocumentFormat", Schema = "Doc")]
    public class DocumentFormat : AuditEntity
    {
        [Required]
        public Guid DocumentFormatTypeId { get; set; }
        [ForeignKey("DocumentFormatTypeId")]

        public DocumentFormatType DocumentFormatType { get; set; } = default!;
        [Required]
        public Guid DocumentSizeId { get; set; }
        [ForeignKey("DocumentSizeId")]

        public DocumentSize DocumentSize { get; set; } = default!;
    }
}