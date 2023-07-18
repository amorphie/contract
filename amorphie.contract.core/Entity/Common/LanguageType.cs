using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("LanguageType", Schema = "Common")]
[Index(nameof(Name), IsUnique = true)]
    public class LanguageType : EntityBase
    {
        public string Name { get; set; }// EN |TR |FR
        
    }
}