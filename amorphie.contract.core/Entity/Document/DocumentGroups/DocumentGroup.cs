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

namespace amorphie.contract.core.Entity.Document.DocumentGroups
{
    [Table("DocumentGroup", Schema = "DocGroup")]
    public class DocumentGroup : BaseEntity
    {

        [Required]
        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<DocumentGroupDetail> DocumentGroupDetails { get; set; } = new List<DocumentGroupDetail>();
        public virtual ICollection<DocumentGroupLanguageDetail> DocumentGroupLanguageDetail { get; set; } = new List<DocumentGroupLanguageDetail>();

    }
}