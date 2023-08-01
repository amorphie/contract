using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
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
        public virtual ICollection<DocumentDefinitionLanguageDetail> DocumentDefinitionLanguageDetails{ get; } = new List<DocumentDefinitionLanguageDetail>();
        // public virtual ICollection<DocumentDefinitionGroupDetail> DocumentDefinitionGroupDetails {get;set;}
        public virtual ICollection<DocumentFormatDetail>? DocumentFormatDetails { get; set; }
        public virtual ICollection<DocumentTemplateDetail>? DocumentTemplateDetails { get; set; }
        public virtual ICollection<DocumentFormIODetail>? DocumentFormIODetail { get; set; }
        public virtual ICollection<DocumentEntityProperty>? DocumentEntityPropertys { get; set; }
        public virtual ICollection<DocumentTag>  DocumentTags { get; } = new List<DocumentTag>();
        public virtual ICollection<DocumentAllowedDetail>?  DocumentAllowedDetails { get; set; }
        
    }
}