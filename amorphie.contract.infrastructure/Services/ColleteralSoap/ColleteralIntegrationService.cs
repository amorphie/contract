using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using amorphie.contract.core;
using amorphie.contract.core.Model.Colleteral;
using amorphie.contract.core.Services;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace amorphie.contract.infrastructure.Services.DysSoap;


public class ColleteralIntegrationService : IColleteralIntegrationService
{
    private readonly ILogger _logger;
    private readonly EndpointAddress endpointAddress;
    private readonly BasicHttpsBinding binding;

    private readonly ColleteralSoapClient colleteralServiceSoapClient;

    public ColleteralIntegrationService(ILogger logger)
    {

        binding = new BasicHttpsBinding();
        binding.Security.Mode = BasicHttpsSecurityMode.Transport;
        endpointAddress = new EndpointAddress(StaticValuesExtensions.Fora.ColleteralUrl);

        colleteralServiceSoapClient = new ColleteralSoapClient(binding, endpointAddress);

        colleteralServiceSoapClient.ChannelFactory.Credentials.ServiceCertificate.SslCertificateAuthentication =
            colleteralServiceSoapClient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck,
                };

        _logger = logger;
    }

    public async Task AddTSIZLDocument(DoAutomaticEngagementPlainRequestDto model)
    {
        if (model.AccountBranchCode <= 0)
        {
            throw new ArgumentException("AccountBranchCode must be greater than zero");
        }

        var result = await colleteralServiceSoapClient.DoAutomaticEngagementPlainAsync(model.AccountBranchCode, model.AccountNumber, model.AccountSuffix, model.CurrencyCode, model.EngagementDate, model.EngagementType, model.EngagementKind, model.EngagementAmount, model.UserCode);

        if (result.HasError)
        {
            _logger.Error("Failed to DoAutomaticEngagementAsync. {Message} - {EngagementModel}", result.ErrorMessage, model.ToString());
            throw new Exception(result.ErrorMessage);
        }

        _logger.Information("DoAutomaticEngagementPlain was sent {EngagementModel} {ReferenceNumber}", model.ToString(), result.ReferenceNumber);
    }
}