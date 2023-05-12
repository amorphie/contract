using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.data.Entity.Base
{
    public abstract class BaseEntity: BaseEntityWithOutId, IHasKey
    {
        // [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


    }
}