using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    }
}