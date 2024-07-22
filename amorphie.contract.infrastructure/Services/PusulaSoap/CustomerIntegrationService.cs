using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using amorphie.contract.core;
using amorphie.contract.core.Model.Pusula;
using amorphie.contract.core.Response;
using amorphie.contract.core.Services;
using Serilog;

namespace amorphie.contract.infrastructure.Services.PusulaSoap;


public class CustomerIntegrationService : ICustomerIntegrationService
{
    private readonly ILogger _logger;
    private readonly EndpointAddress endpointAddress;
    private readonly BasicHttpsBinding binding;

    private readonly CustomerServicesSoapClient customerServicesSoapClient;

    public CustomerIntegrationService(ILogger logger)
    {
        binding = new BasicHttpsBinding();
        binding.Security.Mode = BasicHttpsSecurityMode.Transport;
        endpointAddress = new EndpointAddress(StaticValuesExtensions.Pusula.CustomerServicesUrl);

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
        var customerInfoResponse = new PusulaCustomerInfoResponseModel();

        try
        {
            var result = await customerServicesSoapClient.LoadCustomerGeneralNewAsync(customerNo);
            bool foundMainB = false, foundCitizen = false, foundTaxNo = false;

            foreach (System.Data.DataTable table in result.Tables)
            {
                foreach (System.Data.DataRow row in table.Rows)
                {
                    var mainBranchCode = row["MainBranchCode"];

                    if (mainBranchCode != DBNull.Value)
                    {
                        foundMainB = true;
                        var _mainBranchCode = Convert.ToInt32(mainBranchCode);
                        customerInfoResponse.MainBranchCode = _mainBranchCode;
                    }

                    var citizenshipNumber = row["CitizenshipNumber"];
                    if (citizenshipNumber != DBNull.Value)
                    {
                        foundCitizen = true;
                        var _citizenshipNumber = Convert.ToString(citizenshipNumber);
                        customerInfoResponse.CitizenshipNumber = _citizenshipNumber;
                    }

                    var taxNo = row["TaxNo"];
                    if (taxNo != DBNull.Value)
                    {
                        foundTaxNo = true;
                        var _taxNo = Convert.ToString(taxNo);
                        customerInfoResponse.TaxNo = _taxNo;
                    }

                    if (foundMainB && foundCitizen && foundTaxNo)
                        break;
                }

                if (foundMainB && foundCitizen && foundTaxNo)
                    break;
            }

            return GenericResult<PusulaCustomerInfoResponseModel>.Success(customerInfoResponse);

        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to fetch LoadCustomerGeneralNew {customerNo}", customerNo);
            return GenericResult<PusulaCustomerInfoResponseModel>.Fail($"Failed to fetch LoadCustomerGeneralNew {customerNo} Err:{ex.Message}");
        }
    }
}