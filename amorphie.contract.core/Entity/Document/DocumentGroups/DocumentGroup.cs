using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Entity.Document.DocumentGroups
{
    [Table("DocumentGroup", Schema = "DocGroup")]
    public class DocumentGroup : BaseEntity
    {

        // [Required]
        // public Guid StatusId { get; set; }
        // [ForeignKey("StatusId")]
        public EStatus Status { get; set; } = default!;
        public ICollection<DocumentGroupDetail> DocumentGroupDetails { get; set; } = new List<DocumentGroupDetail>();
        public ICollection<DocumentGroupLanguageDetail> DocumentGroupLanguageDetail { get; set; } = new List<DocumentGroupLanguageDetail>();

    }
}