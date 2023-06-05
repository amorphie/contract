using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.EAV
{
    [Table("EntityPropertyType", Schema = "EAV")]
    public class EntityPropertyType: EntityBase
    {
        public string Code { get; set; }
        
    }
}