using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;
using Microsoft.AspNetCore.Mvc;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentOperations", Schema = "Doc")]
    public class DocumentOperations : EntityBase
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        public bool DocumentManuelControl {get;set;}
        [Required]
        public Guid TagId { get; set; }
        public Tag Tags {get;set;}
    }
}