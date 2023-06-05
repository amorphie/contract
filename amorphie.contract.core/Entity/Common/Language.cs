using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("Language", Schema = "Common")]
    [Index(nameof(Code))]

    public class Language : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<DocumentDefinitionLanguageDetail> DocumentDefinitionLanguageDetails { get; set; }

    }
}