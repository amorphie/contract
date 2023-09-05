using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document.DocumentTypes
{
      [Table("DocumentUpload", Schema = "DocTp")]
    public class DocumentUpload : EntityBase
    {
        public bool Required { get; set; }
        public virtual ICollection<DocumentFormatDetail>? DocumentFormatDetails { get; set; }
        public virtual ICollection<DocumentAllowedDetail>?  DocumentAllowedDetails { get; set; }
    }
}