using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity
{
    public class Customer : AudiEntity
    {
        public string? Owner { get; set; }
        public string? Reference { get; set; }
        public virtual ICollection<Document.Document>? DocumentList { get; set; } = new List<Document.Document>();

    }
}