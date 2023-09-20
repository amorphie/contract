using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

using amorphie.contract.core.Entity.Common;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document.DocumentGroups
{
    // [Index(nameof(MultiLanguageId), IsUnique = true)]
    [Table("DocumentGroupLanguageDetail", Schema = "DocGroup")]
    public class DocumentGroupLanguageDetail : EntityBase
    {
        public Guid MultiLanguageId { get; set; }

        public MultiLanguage? MultiLanguage { get; set; }
        public Guid DocumentGroupId { get; set; }

        public DocumentGroup? DocumentGroup { get; set; }

    }
}