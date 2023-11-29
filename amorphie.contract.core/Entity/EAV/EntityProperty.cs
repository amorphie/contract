using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Enum;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.EAV
{
    [Table("EntityProperty", Schema = "EAV")]
    [Index(nameof(Code), IsUnique = true)]


    public class EntityProperty : EntityBase
    {
        public string Code { get; set; }
        [Required]
        public ushort EEntityPropertyType { get; set; }
        [Required]
        public Guid EntityPropertyValueId { get; set; }
        public EntityPropertyValue EntityPropertyValue { get; set; }

    }
}