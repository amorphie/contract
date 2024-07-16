using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Base;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentMigrationProcessing", Schema = "Doc")]
    public class DocumentMigrationProcessing : EntityBase, ISoftDelete
    {

        private readonly int maxRetryCount = 2;
        private readonly int maxLength = 500;

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

            if (!String.IsNullOrEmpty(errorMessage) && errorMessage.Length > maxLength)
            {
                errorMessage = errorMessage[..maxLength];
            }

            if (!String.IsNullOrEmpty(errorMessage))
            {
                ErrorMessage = errorMessage;
            }
        }

        public void IncreaseTryCount()
        {
            TryCount++;
        }

        public bool IsExceededMaxRetryCount()
        {
            return TryCount > maxRetryCount;
        }
    }

}