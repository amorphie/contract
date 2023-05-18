using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Definition
{
    [Table("DocumentGroupDetail", Schema = "Definition")]

    public class DocumentGroupDetail : EntityBase
    {
        [ForeignKey("DocumentDefinitionId")]
        public DocumentDefinition DocumentDefinition { get; set; }
        [ForeignKey("DocumentGroupId")]
        public DocumentGroup DocumentGroup { get; set; }

    }
}