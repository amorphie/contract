using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentFormat", Schema = "Doc")]
    public class DocumentFormat : EntityBase
    {
        [Required]
        public Guid DocumentFormatTypeId { get; set; }
        public DocumentFormatType DocumentFormatType { get; set; }
        [Required]
        public Guid DocumentSizeId { get; set; }
        public DocumentSize DocumentSize { get; set; }
    }
}