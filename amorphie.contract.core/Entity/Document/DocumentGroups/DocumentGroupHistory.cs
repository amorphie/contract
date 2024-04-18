using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.History;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("DocumentGroupHistory", Schema = "DocGroup")]
    public class DocumentGroupHistory : AuditEntity
    {
        [Required]
        public DocumentGroupHistoryModel DocumentGroupHistoryModel { get; set; }
        public Guid DocumentGroupId { get; set; }
    }
}