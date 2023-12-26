using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;
using Microsoft.AspNetCore.Mvc;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentOperations", Schema = "Doc")]
    public class DocumentOperations : AudiEntity
    {
        public bool DocumentManuelControl { get; set; }
        public   ICollection<DocumentOperationsTagsDetail>? DocumentOperationsTagsDetail { get; set; } = new List<DocumentOperationsTagsDetail>();
        // [Required]
        // public Guid TagId { get; set; }
        // public Tag Tags {get;set;}
    }
}