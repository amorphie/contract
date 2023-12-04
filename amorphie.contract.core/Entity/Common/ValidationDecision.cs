using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("ValidationDecision", Schema = "Common")]

    public class ValidationDecision : BaseEntity
    {
        //  [Required]
        // public Guid ValidationDecisionTypeId { get; set; }
        // public  ValidationDecisionType ValidationDecisionTypes { get; set; }// zeebe-comand-dmn-table - script -c# roslyn– esicion-table

    }
}