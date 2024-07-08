using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.core.Model.Proxy
{
    public class SendTemplatedMailProcessModel
    {
        public string Name { get; set; }
        public string ItemId { get; set; }
        public string Action { get; set; }
        public string Identity { get; set; }
    }
}