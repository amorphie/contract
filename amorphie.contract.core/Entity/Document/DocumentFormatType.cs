using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentFormatType", Schema = "Doc")]
    
    public class DocumentFormatType : BaseEntity
    {
        public string ContentType { get; set; }//ayrılcak ihtiyaca dogru bakacaz
    }
}