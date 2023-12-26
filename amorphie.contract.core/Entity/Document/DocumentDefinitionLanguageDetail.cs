using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentDefinitionLanguageDetail", Schema = "Doc")]
    public class DocumentDefinitionLanguageDetail : AudiEntity
    {
        [Required]
        public Guid MultiLanguageId { get; set; }
        [ForeignKey("MultiLanguageId")]

        public   MultiLanguage MultiLanguage { get; set; } = default!;
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        [ForeignKey("DocumentDefinitionId")]

        public DocumentDefinition DocumentDefinition { get; set; } = default!;
    }
}