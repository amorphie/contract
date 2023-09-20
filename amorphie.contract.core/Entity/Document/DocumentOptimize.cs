using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentOptimize", Schema = "Doc")]
    public class DocumentOptimize : EntityBase
    {
        public Guid DocumentDefinitionId { get; set; }
        public bool Size { get; set; }
        public Guid DocumentOptimizeTypeId { get; set; }
        public DocumentOptimizeType DocumentOptimizeType { get; set; }
    }
}