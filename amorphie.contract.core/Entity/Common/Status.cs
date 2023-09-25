using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("Status", Schema = "Common")]
    [Index(nameof(Code), IsUnique = true)]
    public class Status : EntityBase
    {
        public string Code { get; set; }// active

    }
}