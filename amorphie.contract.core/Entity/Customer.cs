using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity
{
    [Table("Customer", Schema = "Cus")]
    public class Customer : AuditEntity
    {
        public string? Owner { get; set; }
        public string? Reference { get; set; }
        public long? CustomerNo { get; set; }
        public ICollection<Document.Document>? DocumentList { get; set; } = new List<Document.Document>();

    }
}