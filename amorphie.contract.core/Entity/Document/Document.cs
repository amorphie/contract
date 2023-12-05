using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;
using NpgsqlTypes;

namespace amorphie.contract.core.Entity.Document
{
    [Table("Document", Schema = "Doc")]
    public class Document : AudiEntity
    {
        [Required]
        [ForeignKey(nameof(DocumentDefinitionCode))]
        public string DocumentDefinitionCode { get; set; }
        public virtual DocumentDefinition DocumentDefinition { get; set; }
        [Required]
        public Guid DocumentContentId { get; set; }
        public virtual DocumentContent DocumentContent { get; set; }
        public virtual Status Status { get; set; }
        [Required]
        public Guid StatusId { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}