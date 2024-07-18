using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentMigrationDysDocumentTags", Schema = "Doc")]
    public class DocumentMigrationDysDocumentTag : EntityBase, ISoftDelete
    {
        public long DocId { get; set; }
        public string TagId { get; set; } = default!;
        public Dictionary<string, string> TagValues { get; set; } = default!;
        public string TagValuesOrg { get; set; } = default!;
        public bool IsDeleted { get; set; }
    }

}