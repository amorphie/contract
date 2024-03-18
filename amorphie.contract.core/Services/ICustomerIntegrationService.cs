using amorphie.contract.core.Model.Pusula;
using amorphie.contract.core.Response;

namespace amorphie.contract.core.Services
{
    public interface ICustomerIntegrationService
    {
        Task<GenericResult<PusulaCustomerInfoResponseModel>> GetCustomerInfo(long customerNo);
    }
}