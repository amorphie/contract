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
    [Table("ContractProcess", Schema = "Cont")]
    public class ContractProcess : AudiEntity
    {
        public string Client { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Action { get; set; }
    }

}