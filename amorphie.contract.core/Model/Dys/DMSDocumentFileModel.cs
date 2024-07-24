namespace amorphie.contract.core.Model.Dys;

public class DMSDocumentFileModel
{
    public long DocId { get; set; }
    public string FileName { get; set; }
    public string MimeType { get; set; }
    public byte[] FileContent { get; set; }
}
