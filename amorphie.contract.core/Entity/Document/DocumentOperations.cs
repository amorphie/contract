using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentOperations", Schema = "Doc")]
    public class DocumentOperations : EntityBase
    {
         public Guid DocumentDefinitionId { get; set; }
        public bool DocumentManuelControl {get;set;}
        public Guid TagId { get; set; }
        public Tag Tags {get;set;}
    }
}