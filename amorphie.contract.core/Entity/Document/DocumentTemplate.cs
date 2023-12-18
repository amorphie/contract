using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTemplate", Schema = "Doc")]

    public class DocumentTemplate : AudiEntity
    {
        public string Code { get; set; }
        [Required]
        public Guid LanguageTypeId { get; set; }
        public virtual LanguageType LanguageType { get; set; }
        public string Version { get; set; }

    }
}