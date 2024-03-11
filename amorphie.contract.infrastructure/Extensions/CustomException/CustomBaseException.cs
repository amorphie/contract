using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace amorphie.contract.infrastructure.Extensions.CustomException
{
    public abstract class CustomBaseException : Exception
    {
        public virtual string MessageFormat { get; }
        public virtual string Title { get; }
        public virtual HttpStatusCode StatusCode { get; }
        public virtual Dictionary<string, string> MessageProps { get; set; } = new();
    }
}