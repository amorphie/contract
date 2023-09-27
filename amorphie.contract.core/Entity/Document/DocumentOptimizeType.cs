using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentOptimizeType", Schema = "Doc")]
    [Index(nameof(Code), IsUnique = true)]
    public class DocumentOptimizeType : EntityBase
    { 
        public string Code {get;set;}
    }
}