using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentType", Schema = "Doc")]
    [Index(nameof(Name), IsUnique = true)] 
    
    public class DocumentType : EntityBase
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
    }
}