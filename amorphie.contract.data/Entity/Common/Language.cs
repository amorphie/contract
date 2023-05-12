using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.data.Entity.Common
{
    [Table("Language", Schema = "Common")]
    [Index(nameof(Code))]

    public class Language : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}