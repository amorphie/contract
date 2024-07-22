using amorphie.contract.core.Model.Dys;
using amorphie.contract.core.Response;

namespace amorphie.contract.core.Services
{
    public interface IDysIntegrationService
    {
        Task<string> AddDysDocument(DocumentDysRequestModel model);
        Task<GenericResult<DmsDocumentAndFileModel>> GetDocumentAndData(long docId);
    }
}