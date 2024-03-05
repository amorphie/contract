namespace amorphie.contract.core.Model.Dys;

public class DocumentDysRequestModel
{
    public string DocumentTypeDMSReferenceId { get; private set; }
    public string Description { get; private set; }
    public Dictionary<string, string> DocumentParameters { get; set; }
    public string FileName { get; private set; }
    public string MimeType { get; private set; }
    public byte[] Content { get; private set; }

    public DocumentDysRequestModel(string dmsReferenceId, string description, string fileName, string mimeType, byte[] content)
    {
        DocumentTypeDMSReferenceId = dmsReferenceId;
        Description = description;
        FileName = fileName;
        MimeType = mimeType;
        Content = content;
        DocumentParameters = new Dictionary<string, string>();

    }

}