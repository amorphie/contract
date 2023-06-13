using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentNote", Schema = "Doc")]
    public class DocumentNote : EntityBase
    {
        public string Note { get; set; }
        public Guid DocumentId{ get; set; }

        public Document Document { get; set; }
    }
}