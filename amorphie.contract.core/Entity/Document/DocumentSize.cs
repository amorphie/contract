using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
      [Table("DocumentSize", Schema = "Definition")]
    public class DocumentSize : EntityBase
    {
        public ulong KiloBytes { get; set; }
        
    }
}