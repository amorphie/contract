using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Base;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document.DocumentTypes
{
    [Table("DocumentUpload", Schema = "DocTp")]
    public class DocumentUpload : AudiEntity
    {
        public bool Required { get; set; }
        public virtual ICollection<DocumentFormatDetail> DocumentFormatDetails { get; set; } = new List<DocumentFormatDetail>();
        public virtual ICollection<DocumentAllowedClientDetail> DocumentAllowedClientDetails { get; set; } = new List<DocumentAllowedClientDetail>();
    }
}