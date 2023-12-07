using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
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

    public class ContractDocumentDetail : AudiEntity
    {
        [Required]

        [ForeignKey(nameof(ContractDefinitionCode))]
        public string ContractDefinitionCode { get; set; }

        // public ContractDefinition? ContractDefinition { get; set; }
        [Required]
        [ForeignKey(nameof(DocumentDefinitionCode))]
        public string DocumentDefinitionCode { get; set; }
        public virtual DocumentDefinition DocumentDefinition { get; set; }

        public ushort UseExisting { get; set; }
        // public UseExisting? UseExisting { get; set; }//enum
        public string MinVersion { get; set; }
        public bool Required { get; set; }

    }
}