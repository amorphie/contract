using System.ServiceModel;
using System.ServiceModel.Channels;
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
    private readonly Binding binding;

    public DysIntegrationService(ILogger logger)
    {
        endpointAddress = new EndpointAddress(StaticValuesExtensions.DmsUrl);
        binding = new BasicHttpBinding();
        _logger = logger;
    }

    public async Task<string> AddDysDocument(DocumentDysRequestModel model)
    {
        DmsServiceSoapClient dms = new(binding, endpointAddress);

        StringBuilder cmdData = new StringBuilder();
        cmdData.Append("<document>");
        cmdData.Append($"<fileName>{model.FileName}</fileName>");
        cmdData.Append($"<mimeType>{model.MimeType}</mimeType>");
        cmdData.Append("<ownerID>" + "908" + "</ownerID>");
        cmdData.Append("<desc>" + "docTitle" + "  </desc>");
        cmdData.Append("<notes>" + "" + "</notes>");
        cmdData.Append("<channel>" + "AmorphieContract" + "</channel>");
        cmdData.Append("<wfInstanceID></wfInstanceID>");
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

        var dmsdocResult = await dms.AddDocumentAsync(cmdData.ToString(), model.Content);

        _logger.Information("DYS document was created {cmdData} - {AddDocumentResult}", cmdData.ToString(), dmsdocResult.AddDocumentResult);

        return dmsdocResult.AddDocumentResult;
    }
}