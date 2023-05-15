using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;
namespace amorphie.contract.core.Entity.Definition
{
    [Index(nameof(Code))]
    [Table("Document", Schema = "Definition")]
    public class DocumentDefinition : BaseEntity
    {
        [MaxLength(500)]
        [Description("Kode")]
        [Required]
        public string Code { get; set; }


        [Key]
        [MaxLength(36)]
        public Guid LanguageId { get; set; }
        public virtual Language Language { get; set; }
        // public Guid DocumentDefinitionTypeId { get; set; }
        public virtual ICollection<DocumentDefinitionType> DocumentDefinitionType { get; set; }
        public virtual ICollection<DocumentDefinitionTemplate> DocumentDefinitionTemplate { get; set; }
    }
}