using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentDefinition", Schema = "Doc")]
    public class DocumentDefinition : BaseEntity
    {

        public EStatus Status { get; set; } = default!;

        public EStatus BaseStatus { get; set; } = default!;
        public ICollection<DocumentTagsDetail>? DocumentTagsDetails { get; set; } = new List<DocumentTagsDetail>();

        public DocumentDys DocumentDys { get; set; } = default!;
        public DocumentTsizl DocumentTsizl { get; set; } = default!;

        #region documentType
        public Guid? DocumentUploadId { get; set; }
        [ForeignKey("DocumentUploadId")]

        public DocumentUpload? DocumentUpload { get; set; } = default!;
        public Guid? DocumentOnlineSignId { get; set; }
        [ForeignKey("DocumentOnlineSignId")]

        public DocumentOnlineSign? DocumentOnlineSign { get; set; } = default!;

        #endregion
        public Guid? DocumentOptimizeId { get; set; }
        [ForeignKey("DocumentOptimizeId")]
        public DocumentOptimize? DocumentOptimize { get; set; } = default!;
        public Guid? DocumentOperationId { get; set; }
        [ForeignKey("DocumentOperationId")]
        public DocumentOperations? DocumentOperations { get; set; } = default!;
        [Required]
        public string Semver { get; set; } = default!;

        public Dictionary<string, string> Titles { get; set; } = default!;

        public List<Metadata> DefinitionMetadata { get; set; } = new();

        public override string ToString()
        {
            return Code;
        }
    }
}