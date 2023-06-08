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
    [Index(nameof(Code))]
    [Table("DocumentDefinition", Schema = "Doc")]
    public class DocumentDefinition : EntityBase
    {
        [MaxLength(500)]
        [Description("Kode")]
        [Required]
        public string Code { get; set; }
        public virtual ICollection<DocumentDefinitionLanguageDetail> DocumentDefinitionLanguageDetails { get; set; }
        public virtual ICollection<DocumentDefinitionGroupDetail> DocumentDefinitionGroupDetails {get;set;}
        public virtual ICollection<DocumentFormat> DocumentFormats { get; set; }
        public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; }
        public virtual ICollection<DocumentEntityProperty> DocumentEntityPropertys { get; set; }
    }
}