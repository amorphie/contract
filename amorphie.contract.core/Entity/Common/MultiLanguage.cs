using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("MultiLanguage", Schema = "Common")]
    [Index(nameof(Code))]

    public class MultiLanguage : AudiEntity
    {
        public string Name { get; set; }//Nufus Cuzdani
        public string Code { get; set; }//özel durum farkındaysan baseentityden türemiyor
        [Required]
        public Guid LanguageTypeId { get; set; }
        [ForeignKey("LanguageTypeId")]

        public LanguageType LanguageType { get; set; } = default!;


    }
}