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
    // [Index(nameof(MultiLanguageId), IsUnique = true)]
    [Table("DocumentDefinitionGroupLanguageDetail", Schema = "Doc")]
    public class DocumentDefinitionGroupLanguageDetail : EntityBase
    {
        public Guid MultiLanguageId { get; set; }

        public MultiLanguage? MultiLanguage { get; set; }
        public Guid DocumentDefinitionGroupId { get; set; }

        public DocumentDefinitionGroup? DocumentDefinitionGroup { get; set; }

    }
}