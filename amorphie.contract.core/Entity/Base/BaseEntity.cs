using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Base
{
    public class BaseEntity : AuditEntity
    {
        [MaxLength(1000)]
        [Required]
        public string Code { get; set; } = default!;

    }
    public class AuditEntity : EntityBase, ISoftDelete
    {
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
    }

    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        // public DateTime? DeletedAt { get; set; }

    }
}