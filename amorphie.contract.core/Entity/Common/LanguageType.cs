using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("LanguageType", Schema = "Common")]
[Index(nameof(Code), IsUnique = true)]
    public class LanguageType : BaseEntity
    {
        
    }
}