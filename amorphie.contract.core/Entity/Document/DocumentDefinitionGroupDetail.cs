using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentDefinitionGroupDetail", Schema = "Doc")]

    public class DocumentDefinitionGroupDetail : EntityBase
    {
        public Guid DocumentDefinitionId { get; set; }

        public DocumentDefinition DocumentDefinition { get; set; }
        public Guid DocumentGroupId { get; set; }

        public DocumentGroup DocumentGroup { get; set; }

    }
}