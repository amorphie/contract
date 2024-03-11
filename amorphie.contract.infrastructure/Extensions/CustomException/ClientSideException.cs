using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace amorphie.contract.infrastructure.Extensions.CustomException
{
    public sealed class ClientSideException : CustomBaseException
    {
        public override string MessageFormat => "Client : ContractManager - {processName} - {message}";
        public override string Title => "Client Side Error";
        public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

        public ClientSideException(string message, string processName) : base()
        {
            MessageProps.Add("{message}", message);
            MessageProps.Add("{processName}", processName);
        }
    }
}