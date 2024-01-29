using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Base
{
    public class BaseEntity : AudiEntity
    {
        [MaxLength(1000)]
        [Required]
        public string Code { get; set; } = default!;

    }
    public class AudiEntity : EntityBase, ISoftDelete
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