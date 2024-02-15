using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Services;

namespace amorphie.contract.data.Services
{
    public interface IDocumentService
    {
        // Task<string> GetDocumentPath(string objectName, CancellationToken cancellationToken);

    }
    public class DocumentService : IDocumentService
    {
        // public async Task<string> GetDocumentPath(string objectName, CancellationToken token)
        // {
        //     amorphie.contract.core.Services.IMinioService minioService = new MinioService();
        //     return await minioService.GetDocumentUrl(objectName, token);
        // }

    }
}