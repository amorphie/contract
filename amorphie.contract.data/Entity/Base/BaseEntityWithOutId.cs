using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.data.Entity.Base
{
    public abstract class BaseEntityWithOutId
    {
        public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        public Guid CreatedBy { get; set; }
        public Guid? CreatedByBehalfOf { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        public Guid ModifiedBy { get; set; }
        public Guid? ModifiedByBehalfOf { get; set; }
    }
}