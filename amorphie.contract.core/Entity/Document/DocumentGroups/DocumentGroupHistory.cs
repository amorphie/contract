using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Entity.Contract
{
    [Table("DocumentGroupHistory", Schema = "DocGroup")]
    public class DocumentGroupHistory : AudiEntity
    {
        [Required]
        public DocumentGroupHistoryModel DocumentGroupHistoryModel { get; set; }
        public Guid DocumentGroupId { get; set; }
        [ForeignKey("DocumentGroupId")]

        public DocumentGroup DocumentGroup { get; set; } = default!;
    }

    public class DocumentGroupHistoryModel : BaseEntity
    {
        public EStatus Status { get; set; } = default!;
        public ICollection<DocumentGroupDetail> DocumentGroupDetails { get; set; } = new List<DocumentGroupDetail>();
        public ICollection<DocumentGroupLanguageDetail> DocumentGroupLanguageDetail { get; set; } = new List<DocumentGroupLanguageDetail>();
        public ICollection<DocumentGroupHistory>? DocumentGroupHistories { get; set; } = new List<DocumentGroupHistory>();

    }
}