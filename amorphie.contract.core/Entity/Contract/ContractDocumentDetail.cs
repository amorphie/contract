using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("ContractDocumentDetail", Schema = "Cont")]
    // [Index(nameof(ContractDefinitionId,DocumentDefinitionId), IsUnique = true)]

    public class ContractDocumentDetail : EntityBase
    {
        [Required]
        public Guid ContractDefinitionId { get; set; }

        // public ContractDefinition? ContractDefinition { get; set; }
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        public DocumentDefinition DocumentDefinition { get; set; }

        public ushort UseExisting { get; set; }
        // public UseExisting? UseExisting { get; set; }//enum
        public string Semver { get; set; }
        public bool Required { get; set; }

    }
}