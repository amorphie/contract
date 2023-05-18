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
    [Index(nameof(Code))]
    [Table("DocumentGroup", Schema = "Definition")]
    public class DocumentGroup : EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid LanguageId { get; set; }

        public virtual Language Language { get; set; }
        public virtual ICollection<DocumentDefinition> DocumentDefinitions { get; set; }
        public virtual ICollection<DocumentGroup> DocumentGroups { get; set; }

    }
}