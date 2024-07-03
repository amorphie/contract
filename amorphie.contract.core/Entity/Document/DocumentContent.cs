using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentContent", Schema = "Doc")]
    public class DocumentContent : AuditEntity
    {
        public string KiloBytesSize { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public string ContentTransferEncoding { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string MinioObjectName { get; set; } = default!;
    }
}