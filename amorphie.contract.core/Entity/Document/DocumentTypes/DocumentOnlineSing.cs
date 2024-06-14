using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Model.Documents;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document.DocumentTypes
{
    [Table("DocumentOnlineSign", Schema = "DocTp")]
    public class DocumentOnlineSign : AuditEntity
    {
        public bool Required { get; set; }
        public ICollection<DocumentAllowedClientDetail> DocumentAllowedClientDetails { get; set; } = new List<DocumentAllowedClientDetail>();
        public List<Template> Templates { get; set; }  = new();

    }
}