using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.EAV
{
    [Table("EntityProperty", Schema = "EAV")]
    public class EntityProperty: EntityBase
    {
        public string Code { get; set; }
        public Guid EntityPropertyTypeId { get; set; }
        public EntityPropertyType EntityPropertyType { get; set; }
        public Guid EntityPropertyValueId { get; set; }
        public EntityPropertyValue EntityPropertyValue { get; set; }
        
    }
}