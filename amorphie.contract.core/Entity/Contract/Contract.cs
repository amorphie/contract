using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("Contract", Schema = "Cont")]
    public class Contract : AudiEntity
    {
        //todo: burayı contractDef ile bagla confi düzenle 
        public string ContractName { get; set; }
        public string Reference { get; set; }
        public string Owner { get; set; }
        public string CallbackName { get; set; }
        public Guid ProcessId { get; set; }
        public ContractProcess Process { get; set; }
    }
    
}