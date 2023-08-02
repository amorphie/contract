using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Index(nameof(Code), IsUnique = true)]
    [Table("DocumentGroup", Schema = "Doc")]
    public class DocumentDefinitionGroup : EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid MultiLanguageId { get; set; }

        public Guid StatusId { get; set; }
        public Status? Status { get; set; }
        public virtual ICollection<DocumentDefinitionGroupDetail>? DocumentDefinitionGroupDetails { get; set; }
        public virtual ICollection<DocumentDefinitionGroupLanguageDetail>? DocumentDefinitionGroupLanguageDetail { get; set; }

    }
}