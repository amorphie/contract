using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentContent", Schema = "Doc")]
    public class DocumentContent : EntityBase
    {
        public string ContentData { get; set; }
        public string KiloBytesSize {get;set;}
        public Guid VersionsId{ get; set; }

        public Versions? Versions { get; set; }
    }
}