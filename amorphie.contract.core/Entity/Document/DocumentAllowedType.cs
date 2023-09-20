using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentAllowedType", Schema = "Doc")]
    [Index(nameof(Code), IsUnique = true)] 

    public class DocumentAllowedType : EntityBase
    {
        public string Code { get; set; }
    }
}