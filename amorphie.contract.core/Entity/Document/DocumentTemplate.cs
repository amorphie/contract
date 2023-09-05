using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTemplate", Schema = "Doc")]
    [Index(nameof(Name), IsUnique = true)]

    public class DocumentTemplate : EntityBase
    {
        //Render edilecekler
        public string Name { get; set; }
        public Guid LanguageTypeId { get; set; }
        public LanguageType LanguageType { get; set; }

    }
}