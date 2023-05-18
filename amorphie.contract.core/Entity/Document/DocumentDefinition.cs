using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Entity.Document.DocumentLanguage;

namespace amorphie.contract.core.Entity.Document
{
    [Index(nameof(Code))]
    [Table("DocumentDefinition", Schema = "Definition")]
    public class DocumentDefinition : EntityBase
    {
        [MaxLength(500)]
        [Description("Kode")]
        [Required]
        public string Code { get; set; }

        // public Guid LanguageId{ get; set; }

        // public virtual Language Language { get; set; }
        public virtual ICollection<DocumentDefinitionLanguageDetail> DocumentDefinitionLanguageDetails { get; set; }

        public virtual ICollection<DocumentDefinitionGroupDetail> DocumentDefinitionGroupDetails {get;set;}
        public virtual ICollection<DocumentFormat> DocumentFormats { get; set; }
        public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; }
    }
}