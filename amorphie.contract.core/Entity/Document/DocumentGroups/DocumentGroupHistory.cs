using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using amorphie.contract.core.Entity.Document.DocumentGroups;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("DocumentGroupHistory", Schema = "DocGroup")]
    public class DocumentGroupHistory : AudiEntity
    {
        [Required]
        public string History { get; set; }
        public Guid DocumentGroupId { get; set; }
        [ForeignKey("DocumentGroupId")]

        public DocumentGroup DocumentGroup { get; set; } = default!;
    }
}