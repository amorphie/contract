using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTemplate", Schema = "Doc")]

    public class DocumentTemplate : BaseEntity
    { 
        [Required]
        public Guid LanguageTypeId { get; set; }
        public LanguageType LanguageType { get; set; }

    }
}