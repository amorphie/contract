using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentAllowed", Schema = "Doc")]
    [Index(nameof(Code), IsUnique = true)] 
    public class DocumentAllowed : EntityBase
    {
        //Render edilecekler
        public string Code { get; set; }
        public DocumentAllowedType? DocumentAllowedType { get; set; }//client,
    }
}