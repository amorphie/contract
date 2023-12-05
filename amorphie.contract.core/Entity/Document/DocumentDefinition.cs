using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document.DocumentTypes;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentDefinition", Schema = "Doc")]
    public class DocumentDefinition : BaseEntity
    {

        public virtual Status Status { get; set; }
        [Required]
        public Guid StatusId { get; set; }
        public virtual Status BaseStatus { get; set; }
        [Required]
        public Guid BaseStatusId { get; set; }
        public virtual ICollection<DocumentDefinitionLanguageDetail>? DocumentDefinitionLanguageDetails { get; set; } = new List<DocumentDefinitionLanguageDetail>();
        public virtual ICollection<DocumentEntityProperty>? DocumentEntityPropertys { get; set; } = new List<DocumentEntityProperty>();
        public virtual ICollection<DocumentTagsDetail>? DocumentTagsDetails { get; set; } = new List<DocumentTagsDetail>();

        #region documentType
        public virtual Guid? DocumentUploadId { get; set; }
        public virtual DocumentUpload? DocumentUpload { get; set; }
        // public virtual Guid? DocumentRenderId { get; set; }
        // public virtual DocumentRender? DocumentRender { get; set; }
        public virtual Guid? DocumentOnlineSingId { get; set; }
        public virtual DocumentOnlineSing? DocumentOnlineSing { get; set; }


        #endregion
        public virtual Guid? DocumentOptimizeId { get; set; }
        public virtual DocumentOptimize? DocumentOptimize { get; set; }
        public virtual Guid? DocumentOperationsId { get; set; }
        public virtual DocumentOperations? DocumentOperations { get; set; }

        // public string StartingTransitionName{ get; set; }
        public override string ToString()
        {
            return Code;
        }
    }
}