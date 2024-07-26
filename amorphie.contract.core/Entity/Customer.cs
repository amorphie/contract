using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity
{
    [Table("Customer", Schema = "Cus")]
    public class Customer : AuditEntity
    {
        public string? Owner { get; set; }
        public string Reference { get; set; } = default!;
        public long? CustomerNo { get; set; }
        public ICollection<Document.Document>? DocumentList { get; set; } = [];

        [MaxLength(10)]
        public string? TaxNo { get; set; }
    }
}