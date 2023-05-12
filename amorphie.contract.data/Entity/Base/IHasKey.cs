using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.data.Entity.Base
{
    public interface IHasKey
    {

        Guid Id { get; set; }
    }
}