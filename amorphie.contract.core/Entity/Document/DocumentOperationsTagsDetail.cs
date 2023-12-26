using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentOperationsTagsDetail", Schema = "Doc")]

    public class DocumentOperationsTagsDetail : AudiEntity
    {
        [Required]
        public Guid? DocumentOperationsId { get; set; }
        [ForeignKey("DocumentOperationsId")]

        public DocumentOperations DocumentOperations { get; set; } = default!;
        [Required]

        public Guid TagId { get; set; }
        [ForeignKey("TagId")]

        public   Common.Tag Tags { get; set; } = default!;

    }
}