using amorphie.contract.core.Model.Colleteral;

namespace amorphie.contract.core.Services
{
    public interface IColleteralIntegrationService
    {
        Task AddTSIZLDocument(DoAutomaticEngagementPlainRequestDto model);
    }
}