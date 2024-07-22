using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentMigrationDysDocuments", Schema = "Doc")]
    public class DocumentMigrationDysDocument : EntityBase, ISoftDelete
    {
        public long DocId { get; set; }
        public string Title { get; set; } = default!;
        public string Notes { get; set; } = default!;
        public DateTime DocCreatedAt { get; set; }
        public string OwnerId { get; set; } = default!;
        public string Channel { get; set; } = default!;
        public bool IsDeleted { get; set; }
    }
  
}