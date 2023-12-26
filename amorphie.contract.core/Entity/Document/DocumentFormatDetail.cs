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
        public Guid DocumentDefinitionId { get; set; }
        [ForeignKey("DocumentDefinitionId")]

        public DocumentDefinition DocumentDefinition { get; set; } = default!;
        [Required]
        public Guid DocumentFormatId { get; set; }
        [ForeignKey("DocumentFormatId")]

        public DocumentFormat DocumentFormat { get; set; } = default!;

    }
}