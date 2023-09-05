using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document.DocumentTypes
{
      [Table("DocumentOnlineSing", Schema = "DocTp")]
    public class DocumentOnlineSing : EntityBase
    {
        public bool Required { get; set; }
        public virtual ICollection<DocumentAllowedDetail>?  DocumentAllowedDetails { get; set; }
        public virtual ICollection<DocumentTemplateDetail>? DocumentTemplateDetails { get; set; }
        public Guid DocumentVersionsId { get; set; }  
        public DocumentVersions DocumentVersions { get; set; }
    }
}