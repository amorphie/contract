namespace amorphie.contract.core.Services
{
    public interface IPdfManager
    {
        bool VerifyPdfContent(string originalContent, string signedContent);
    }
}