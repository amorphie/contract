using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using amorphie.contract.core.Model.Pusula;
using amorphie.contract.core.Response;
using amorphie.contract.core.Services;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace amorphie.contract.infrastructure.Services.PusulaSoap;


public class CustomerIntegrationService : ICustomerIntegrationService
{
    private readonly ILogger _logger;
    private readonly EndpointAddress endpointAddress;
    private readonly BasicHttpsBinding binding;

    private readonly CustomerServicesSoapClient customerServicesSoapClient;
    private readonly IConfiguration _configuration;

    public CustomerIntegrationService(ILogger logger, IConfiguration configuration)
    {
        _configuration = configuration;

        binding = new BasicHttpsBinding();
        binding.Security.Mode = BasicHttpsSecurityMode.Transport;
        endpointAddress = new EndpointAddress(_configuration["Pusula:CustomerServicesUrl"]);

        customerServicesSoapClient = new CustomerServicesSoapClient(binding, endpointAddress);

        customerServicesSoapClient.ChannelFactory.Credentials.ServiceCertificate.SslCertificateAuthentication =
            customerServicesSoapClient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck,
                };

        _logger = logger;
    }

    public async Task<GenericResult<PusulaCustomerInfoResponseModel>> GetCustomerInfo(long customerNo)
    {

        var result = await customerServicesSoapClient.LoadCustomerGeneralNewAsync(customerNo);

        foreach (System.Data.DataTable table in result.Tables)
        {
            foreach (System.Data.DataRow row in table.Rows)
            {
                var mainBranchCode = row["MainBranchCode"];

                if (mainBranchCode != DBNull.Value)
                {
                    var _mainBranchCode = Convert.ToInt32(mainBranchCode);
                    return GenericResult<PusulaCustomerInfoResponseModel>.Success(new PusulaCustomerInfoResponseModel(_mainBranchCode));
                }
            }
        }

        _logger.Error("MainBranchCode not found. {customerNo}", customerNo);
        return GenericResult<PusulaCustomerInfoResponseModel>.Fail("MainBranchCode not found");
    }
}