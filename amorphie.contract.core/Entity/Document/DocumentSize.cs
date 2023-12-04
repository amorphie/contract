using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
      [Table("DocumentSize", Schema = "Doc")]
    public class DocumentSize : AudiEntity
    {
        [Required]
        public ulong KiloBytes { get; set; }
        
    }
}