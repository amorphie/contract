using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;

using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Index(nameof(Code), IsUnique = true)]
    [Table("DocumentDefinition", Schema = "Doc")]
    public class DocumentDefinition : EntityBase
    {
        [MaxLength(500)]
        [Description("Kode")]
        [Required]
        public string Code { get; set; } //Unique olmali
        public Status Status { get; set; }  
        public Guid StatusId { get; set; }  
        public Status BaseStatus { get; set; }  
        public Guid BaseStatusId { get; set; }  
        public virtual ICollection<DocumentDefinitionLanguageDetail>? DocumentDefinitionLanguageDetails { get; set; } // bu güzel
        // public virtual ICollection<MultiLanguage>? MultiLanguage { get; set; }//bu olmaz
        public virtual ICollection<DocumentFormatDetail>? DocumentFormatDetails { get; set; }
        public virtual ICollection<DocumentTemplateDetail>? DocumentTemplateDetails { get; set; }
        public virtual ICollection<DocumentFormIODetail>? DocumentFormIODetail { get; set; }
        public virtual ICollection<DocumentEntityProperty>? DocumentEntityPropertys { get; set; }
        public virtual ICollection<DocumentTag>?  DocumentTags { get; set;} 
        public virtual ICollection<DocumentAllowedDetail>?  DocumentAllowedDetails { get; set; }
        public override string ToString()
        {
            return Code;
        }
    }
}