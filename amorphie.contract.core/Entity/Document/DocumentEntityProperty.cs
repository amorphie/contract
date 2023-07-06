using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.EAV;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentEntityProperty", Schema = "Doc")]
    public class DocumentEntityProperty : EntityBase
    {
        
        public Guid DocumentDefinitionId { get; set; }
        public DocumentDefinition DocumentDefinition { get; set; }
        public Guid EntityPropertyId { get; set; }
        public virtual EntityProperty EntityProperty { get; set; }

    }
}