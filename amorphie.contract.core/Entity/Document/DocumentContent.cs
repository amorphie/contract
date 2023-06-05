using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentContent", Schema = "Doc")]
    public class DocumentContent : EntityBase
    {
        public string ContentData { get; set; }
        public Guid DocumentVersionsId{ get; set; }

        public DocumentVersions DocumentVersions { get; set; }
    }
}