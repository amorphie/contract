using System.Text;

namespace amorphie.contract.core.Model.Dys;

public class DocumentDysRequestModel
{
    public string DocumentCode { get; set; }
    public string DocumentTypeDMSReferenceId { get; set; }
    public string Description { get; set; }
    public Dictionary<string, string> DocumentParameters { get; set; }
    public string FileName { get; set; }
    public string MimeType { get; set; }
    public byte[] Content { get; set; }
    public DocumentDysRequestModel()
    { }
    public DocumentDysRequestModel(string userReference, string documentCode, string dmsReferenceId, string description, string fileName, string mimeType, byte[] content)
    {
        ArgumentNullException.ThrowIfNull(userReference);

        DocumentCode = documentCode;
        DocumentTypeDMSReferenceId = dmsReferenceId;
        Description = description;
        FileName = fileName;
        MimeType = mimeType;
        Content = content;
        DocumentParameters = new Dictionary<string, string>
        {
            { "Field08TCKimlik", userReference }
        };
    }

    public string ConstructDocumentTags()
    {
        var tags = new StringBuilder();

        foreach (var item in DocumentParameters)
        {
            tags = tags.AppendLine($"<{item.Key}>{item.Value}</{item.Key}>");
        }
        return $"{tags}";
    }

}