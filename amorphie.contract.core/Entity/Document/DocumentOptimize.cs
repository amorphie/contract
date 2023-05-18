using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentOptimize", Schema = "Definition")]
    public class DocumentOptimize : EntityBase
    {
        public bool Size {get;set;}
        public string Transform {get;set;}
    }
}