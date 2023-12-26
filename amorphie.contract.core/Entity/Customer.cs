using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity
{
    [Table("Customer", Schema = "Cus")]
    public class Customer : AudiEntity
    {
        public string? Owner { get; set; }
        public string? Reference { get; set; }
        public ICollection<Document.Document>? DocumentList { get; set; } = new List<Document.Document>();

    }
}