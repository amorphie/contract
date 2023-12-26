using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity.Document.DocumentGroups
{
    // [Index(nameof(MultiLanguageId), IsUnique = true)]
    [Table("DocumentGroupLanguageDetail", Schema = "DocGroup")]
    public class DocumentGroupLanguageDetail : AudiEntity
    {
        [Required]
        public Guid MultiLanguageId { get; set; }
        [ForeignKey("MultiLanguageId")]

        public MultiLanguage MultiLanguage { get; set; } = default!;

        [Required]


        public Guid DocumentGroupId { get; set; }
        [ForeignKey("DocumentGroupId")]

        public DocumentGroup DocumentGroup { get; set; } = default!;

    }
}