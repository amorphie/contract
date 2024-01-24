using amorphie.contract.core.Entity.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract.Request
{
    public class ContractRequest
    {
        public string ContractName { get; set; }
        public string Reference { get; set; }
        //public string Owner { get; set; }
        //public string CallbackName { get; set; }
        //public Guid ProcessId { get; set; }
        //public ContractProcess Process { get; set; } = default!;
    }
}
