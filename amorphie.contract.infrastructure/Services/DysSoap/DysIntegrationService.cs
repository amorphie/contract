using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using amorphie.contract.core;
using amorphie.contract.core.Model.Dys;
using amorphie.contract.core.Services;
using Serilog;

namespace amorphie.contract.infrastructure.Services.DysSoap;


public class DysIntegrationService : IDysIntegrationService
{
    private readonly ILogger _logger;
    private readonly EndpointAddress endpointAddress;
    private readonly BasicHttpsBinding binding;

    private readonly DmsServiceSoapClient dmsServiceSoapClient;

    public DysIntegrationService(ILogger logger)
    {
        binding = new BasicHttpsBinding();
        binding.Security.Mode = BasicHttpsSecurityMode.Transport;
        endpointAddress = new EndpointAddress(StaticValuesExtensions.DmsUrl);

        dmsServiceSoapClient = new DmsServiceSoapClient(binding, endpointAddress);

        dmsServiceSoapClient.ChannelFactory.Credentials.ServiceCertificate.SslCertificateAuthentication =
            dmsServiceSoapClient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck,
                };

        _logger = logger;

        _logger.Information("StaticValuesExtensions.DmsUrl {DmsUrl}}", StaticValuesExtensions.DmsUrl);
    }

    public async Task<string> AddDysDocument(DocumentDysRequestModel model)
    {

        StringBuilder cmdData = new();
        cmdData.Append("<document>");
        cmdData.Append($"<fileName>{model.FileName}</fileName>");
        cmdData.Append($"<mimeType>{model.MimeType}</mimeType>");
        cmdData.Append("<ownerID>EBT\\CONTRACT</ownerID>");
        cmdData.Append($"<desc>{model.DocumentCode}</desc>");
        cmdData.Append("<notes>" + "" + "</notes>");
        cmdData.Append("<channel>" + "Contract" + "</channel>");
        cmdData.Append("<tagInfo>");
        cmdData.Append("<tagInfo>");
        cmdData.Append("<tagList>");
        cmdData.Append($"<tag type=\"tag\">{model.DocumentTypeDMSReferenceId}</tag>");
        cmdData.Append("</tagList>");
        cmdData.Append("<tagData>");
        cmdData.Append($"<tag type=\"data\" id=\"{model.DocumentTypeDMSReferenceId}\">");
        cmdData.Append($"<M{model.DocumentTypeDMSReferenceId}>");
        cmdData.Append(model.ConstructDocumentTags());
        cmdData.Append($"</M{model.DocumentTypeDMSReferenceId}>");
        cmdData.Append("</tag>");
        cmdData.Append("</tagData>");
        cmdData.Append("</tagInfo>");
        cmdData.Append("</tagInfo>");
        cmdData.Append("</document>");

        _logger.Information("DYS document is creating {cmdData} - {content}", cmdData.ToString(), model.Content);

        var dmsdocResult = await dmsServiceSoapClient.AddDocumentAsync(cmdData.ToString(), model.Content);

        _logger.Information("DYS document was created {cmdData} - {AddDocumentResult}", cmdData.ToString(), dmsdocResult.AddDocumentResult);

        return dmsdocResult.AddDocumentResult;
    }
}