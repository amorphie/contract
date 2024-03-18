using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("DocumentGroupHistory", Schema = "DocGroup")]
    public class DocumentGroupHistory : AudiEntity
    {
        public JsonDocument History { get; set; }
        public Guid ContractDefinitionId { get; set; }
        [ForeignKey("ContractDefinitionId")]
        public ContractDefinition ContractDefinition { get; set; } = default!;
    }
}