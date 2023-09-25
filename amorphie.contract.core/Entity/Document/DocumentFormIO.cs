using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentFormIO", Schema = "Doc")]
    [Index(nameof(Code), IsUnique = true)]

    public class DocumentFormIO : EntityBase
    {
        //Render edilecekler
        public string Code { get; set; }
        public string Data { get; set; }
        [Required]
        public Guid LanguageTypeId { get; set; }
        public LanguageType LanguageType { get; set; }
    }
}