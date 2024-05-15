using amorphie.contract.core.Model.Minio;
using Minio.DataModel;

namespace amorphie.contract.core.Services
{
    public interface IMinioService
    {
        Task UploadFile(UploadFileModel uploadFileModel);
        // Task UploadFile();
        Task<string> GetDocumentUrl(string objectName, CancellationToken token);

        Task<GetMinioObjectModel> DownloadFile(string objectName, CancellationToken cancellationToken);
    }
}