using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentMigrationProcessing", Schema = "Doc")]
    public class DocumentMigrationProcessing : EntityBase, ISoftDelete
    {

        public long DocId { get; set; }
        public required string TagId { get; set; } = default!;
        public required string Status { get; set; } = default!;
        public int TryCount { get; set; }
        public virtual DateTime? LastTryTime { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsDeleted { get; set; }

        public void ChangeStatus(string status, string? errorMessage = "")
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(status, nameof(status));

            Status = status;
            ErrorMessage = errorMessage;
        }

        public void IncreaseTryCount()
        {
            TryCount++;
        }
    }

}