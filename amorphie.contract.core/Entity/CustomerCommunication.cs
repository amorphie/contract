using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Base;

namespace amorphie.contract.core.Entity
{
    [Table("CustomerCommunication", Schema = "Cus")]
    public class CustomerCommunication : AuditEntity
    {
        public Guid CustomerId { get; set; }
        public string EmailAddress { get; set; }
        public List<string> DocumentList { get; set; }
        public bool IsSuccess { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}