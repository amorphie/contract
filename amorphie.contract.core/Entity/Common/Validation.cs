using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Enum;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("Validation", Schema = "Common")]
    [Index(nameof(EValidationType), IsUnique = true)]

    public class Validation : AudiEntity
    {
        [Required]
        public EValidationType EValidationType { get; set; }// all-valid
        public Guid? ValidationDecisionId { get; set; }
        [ForeignKey("ValidationDecisionId")]

        public ValidationDecision ValidationDecision { get; set; } = default!;

    }
}