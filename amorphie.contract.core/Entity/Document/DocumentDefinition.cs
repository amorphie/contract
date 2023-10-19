using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.core.Base;

using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Index(nameof(Code), IsUnique = true)]
    [Table("DocumentDefinition", Schema = "Doc")]
    public class DocumentDefinition : EntityBase
    {
        [MaxLength(500)]
        [Description("Kode")]
        [Required]
        public string Code { get; set; } //buna göre getirile biline bilir.
        public Status Status { get; set; }
        [Required]
        public Guid StatusId { get; set; }
        public Status BaseStatus { get; set; }
        [Required]

        public Guid BaseStatusId { get; set; }
        public virtual ICollection<DocumentDefinitionLanguageDetail>? DocumentDefinitionLanguageDetails { get; set; } // bu güzel
        public virtual ICollection<DocumentEntityProperty>? DocumentEntityPropertys { get; set; }//****
        public virtual ICollection<DocumentTagsDetail>? DocumentTagsDetails { get; set; }

        #region documentType
        public virtual Guid? DocumentUploadId { get; set; }
        public virtual DocumentUpload? DocumentUpload { get; set; }
        public virtual Guid? DocumentRenderId { get; set; }
        public virtual DocumentRender? DocumentRender { get; set; }
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