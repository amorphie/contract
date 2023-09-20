using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("Versions", Schema = "Common")]
    [Index(nameof(Code), IsUnique = true)] 

    public class  Versions: EntityBase
    {
        public string Code { get; set; }// 1.1
    }
}