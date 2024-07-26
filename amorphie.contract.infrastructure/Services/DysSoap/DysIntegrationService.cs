using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using amorphie.contract.core;
using amorphie.contract.core.Model.Dys;
using amorphie.contract.core.Response;
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
    }

    public async Task<string> AddDysDocument(DocumentDysRequestModel model)
    {

        StringBuilder cmdData = new();
        cmdData.Append("<document>");
        cmdData.Append($"<fileName>{model.FileName}</fileName>");
        cmdData.Append($"<mimeType>{model.MimeType}</mimeType>");
        cmdData.Append($"<ownerID>{StaticValuesExtensions.Fora.UserCode}</ownerID>");
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

        var dmsdocResult = await dmsServiceSoapClient.AddDocumentAsync(cmdData.ToString(), model.Content);

        _logger.Information("DYS document was created {cmdData} - {AddDocumentResult}", cmdData.ToString(), dmsdocResult.AddDocumentResult);

        return dmsdocResult.AddDocumentResult;
    }

    public async Task<GenericResult<DmsDocumentAndFileModel>> GetDocumentAndData(long docId)
    {
        var getDmsDocumentTask = dmsServiceSoapClient.GetDMSDocumentAsync(docId);

        var getDmsDocumentFileTask = dmsServiceSoapClient.GetDocumentAsync(Convert.ToString(docId));

        await Task.WhenAll(getDmsDocumentTask, getDmsDocumentFileTask);

        var document = await getDmsDocumentTask;
        var docModel = new DMSDocumentModel
        {
            ApplicationNo = document.ApplicationNo,
            Channel = document.Channel,
            CustomerNo = document.CustomerNo,
            DocCreatedAt = document.CreateTime,
            TagId = document.TagID,
            DocId = document.ID,
            IsExpired = document.IsExpired,
            Notes = document.Notes,
            OwnerId = document.OwnerID,
            Title = document.Title,
            WfInstanceID = document.WfInstanceID,
        };

        var documentFile = await getDmsDocumentFileTask;
        var docFileModel = new DMSDocumentFileModel
        {
            DocId = documentFile.FInfo.DocID,
            FileContent = documentFile.BinaryData,
            FileName = documentFile.FInfo.FileName,
            MimeType = documentFile.FInfo.MimeType,
        };

        var result = new DmsDocumentAndFileModel(docModel, docFileModel);

        return GenericResult<DmsDocumentAndFileModel>.Success(result);
    }
}