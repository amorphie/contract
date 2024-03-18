using System.Net;

namespace amorphie.contract.core.CustomException;

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
