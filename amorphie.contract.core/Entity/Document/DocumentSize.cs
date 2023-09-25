using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
      [Table("DocumentSize", Schema = "Doc")]
    public class DocumentSize : EntityBase
    {
        [Required]
        public ulong KiloBytes { get; set; }
        
    }
}