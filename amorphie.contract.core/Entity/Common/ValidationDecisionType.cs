using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("ValidationDecisionType", Schema = "Common")]
    [Index(nameof(Code), IsUnique = true)]

    public class ValidationDecisionType : EntityBase
    {
        [Required]
        public string Code { get; set; }// dmn-ekyc-document-validation

    }
}