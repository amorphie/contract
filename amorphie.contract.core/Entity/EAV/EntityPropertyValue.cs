using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.EAV
{
    [Table("EntityPropertyValue", Schema = "EAV")]
    public class EntityPropertyValue : AudiEntity
    {
        [Required]
        public string Data { get; set; } = default!;
    }
}