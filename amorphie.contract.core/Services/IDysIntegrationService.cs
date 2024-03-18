using amorphie.contract.core.Model.Dys;

namespace amorphie.contract.core.Services
{
    public interface IDysIntegrationService
    {
        Task<string> AddDysDocument(DocumentDysRequestModel model);
    }
}