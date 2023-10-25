using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;
using NpgsqlTypes;

namespace amorphie.contract.core.Entity.Document
{
    [Table("Document", Schema = "Doc")]
    public class Document : EntityBase
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        public DocumentDefinition DocumentDefinition { get; set; }
        [Required]
        public Guid DocumentContentId { get; set; }
        public DocumentContent DocumentContent { get; set; }
        public Status Status { get; set; }
        [Required]
        public Guid StatusId { get; set; }
    }
}