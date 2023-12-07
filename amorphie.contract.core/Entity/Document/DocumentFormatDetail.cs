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
    [Table("DocumentFormatDetail", Schema = "Doc")]

    public class DocumentFormatDetail : AudiEntity
    {
        [Required]
        [ForeignKey(nameof(DocumentDefinitionCode))]
        public string DocumentDefinitionCode { get; set; }
        // public DocumentDefinition? DocumentDefinition { get; set; }
        [Required]
        public Guid DocumentFormatId { get; set; }
        public virtual DocumentFormat DocumentFormat { get; set; }

    }
}