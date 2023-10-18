using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.EAV
{
    [Table("EntityPropertyType", Schema = "EAV")]
    [Index(nameof(Code), IsUnique = true)]

    public class EntityPropertyType : EntityBase
    {
        public string Code { get; set; }

    }
}