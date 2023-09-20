using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("Callback", Schema = "Common")]
    [Index(nameof(Code), IsUnique = true)]

    public class Callback : EntityBase
    {
        public  string Code { get; set; }// dmn-ekyc-document-validation
        public  string Url { get; set; }
        public string? Token { get; set; }

    }
}