using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document.DocumentTypes
{
    [Table("DocumentOnlineSing", Schema = "DocTp")]
    public class DocumentOnlineSing : EntityBase
    {
        public bool Required { get; set; }
        public virtual ICollection<DocumentAllowedClientDetail> DocumentAllowedClientDetails { get; set; } = new List<DocumentAllowedClientDetail>();
        public virtual ICollection<DocumentTemplateDetail> DocumentTemplateDetails { get; set; } = new List<DocumentTemplateDetail>();
        public string Semver { get; set; }
    }
}