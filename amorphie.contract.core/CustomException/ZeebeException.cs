using System.Net;

namespace amorphie.contract.core.CustomException;

public sealed class ZeebeException : CustomBaseException
{
    public override string MessageFormat => "Zeebe : ContractManager - {processName} - {message}";
    public override string Title => "Zeebe Error";
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

    public ZeebeException(string message, string processName) : base()
    {
        MessageProps.Add("{message}", message);
        MessageProps.Add("{processName}", processName);
    }
}
