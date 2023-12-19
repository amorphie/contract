using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentContent", Schema = "Doc")]
    public class DocumentContent : AudiEntity
    {
        [Required]

        public string ContentData { get; set; }
        public string? KiloBytesSize { get; set; }
        public string? ContentType { get; set; }
        public string? ContentTransferEncoding { get; set; }
        public string? Name { get; set; }
        public string MinioObjectName { get; set; }
    }
}