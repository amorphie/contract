using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Enum;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.EAV
{
    [Table("EntityProperty", Schema = "EAV")]


    public class EntityProperty : BaseEntity
    {
        [Required]
        public EEntityPropertyType EEntityPropertyType { get; set; }
        [Required]
        public Guid EntityPropertyValueId { get; set; }
        [ForeignKey("EntityPropertyValueId")]
        public EntityPropertyValue EntityPropertyValue { get; set; } = default!;
        [Required]
        public bool Required { get; set; }

    }
}