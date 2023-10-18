using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTemplate", Schema = "Doc")]
    [Index(nameof(Code), IsUnique = true)]

    public class DocumentTemplate : EntityBase
    {
        public string Code { get; set; }
        [Required]
        public Guid LanguageTypeId { get; set; }
        public LanguageType LanguageType { get; set; }

    }
}