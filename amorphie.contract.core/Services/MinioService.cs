namespace amorphie.contract.core.Services
{
    public interface IMinioService
    {
        Task UploadFile(byte[] data, string objectName, string contentType, string customMetadata);
        Task UploadFile();
        Task<string> GetDocumentUrl(string objectName, CancellationToken token);
    
    }
}