using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("MultiLanguage", Schema = "Common")]
    [Index(nameof(Code))]

    public class MultiLanguage : EntityBase
    {
        public string Name { get; set; }//Nufus Cuzdani
        public string Code { get; set; }//identification-certificate-nc
        [Required]
        public Guid LanguageTypeId { get; set; }
        public LanguageType LanguageType { get; set; }


    }
}