using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("Language", Schema = "Common")]
    [Index(nameof(Code))]

    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}