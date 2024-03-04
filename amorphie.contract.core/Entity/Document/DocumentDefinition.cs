using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentDefinition", Schema = "Doc")]
    public class DocumentDefinition : BaseEntity
    {

        // public Guid StatusId { get; set; }

        // [ForeignKey("StatusId")]

        public EStatus Status { get; set; } = default!;
        // public Guid BaseStatusId { get; set; }

        // [Required]
        // [ForeignKey("BaseStatusId")]

        public EStatus BaseStatus { get; set; } = default!;
        [Required]
        public ICollection<DocumentDefinitionLanguageDetail>? DocumentDefinitionLanguageDetails { get; set; } = new List<DocumentDefinitionLanguageDetail>();
        public ICollection<DocumentEntityProperty>? DocumentEntityPropertys { get; set; } = new List<DocumentEntityProperty>();
        public ICollection<DocumentTagsDetail>? DocumentTagsDetails { get; set; } = new List<DocumentTagsDetail>();

        public DocumentDys DocumentnDys { get; set; } = default!;
        public DocumentTsizl DocumentTsizl { get; set; } = default!;

        #region documentType
        public Guid? DocumentUploadId { get; set; }
        [ForeignKey("DocumentUploadId")]

        public DocumentUpload? DocumentUpload { get; set; } = default!;
        public Guid? DocumentOnlineSingId { get; set; }
        [ForeignKey("DocumentOnlineSingId")]

        public DocumentOnlineSing? DocumentOnlineSing { get; set; } = default!;

        #endregion
        public Guid? DocumentOptimizeId { get; set; }
        [ForeignKey("DocumentOptimizeId")]
        public DocumentOptimize? DocumentOptimize { get; set; } = default!;
        public Guid? DocumentOperationId { get; set; }
        [ForeignKey("DocumentOperationId")]
        public DocumentOperations? DocumentOperations { get; set; } = default!;
        [Required]
        public string Semver { get; set; } = default!;

        public override string ToString()
        {
            return Code;
        }
    }
}