namespace amorphie.contract.core.Model.Dys;

public class DMSDocumentModel
{
    public string CustomerNo { get; set; } = default!;
    public string ApplicationNo { get; set; } = default!;
    public string WfInstanceID { get; set; } = default!;
    public string TagId { get; set; } = default!;
    public bool IsExpired { get; set; }
    public long DocId { get; set; }
    public string Title { get; set; } = default!;
    public string Notes { get; set; } = default!;
    public DateTime DocCreatedAt { get; set; }
    public string OwnerId { get; set; } = default!;
    public string Channel { get; set; } = default!;
    public bool IsDeleted { get; set; }

}

public class DMSDocumentFileModel
{
    // <MetaData>string</MetaData>
    // <FInfo>
    //   <ServerID>string</ServerID>
    //   <Library>string</Library>
    //   <Cabinet>string</Cabinet>
    //   <Folder>string</Folder>
    //   <ID>string</ID>
    //   <DocID>long</DocID>
    //   <MimeType>string</MimeType>
    //   <FileName>string</FileName>
    //   <IsLegacy>boolean</IsLegacy>
    //   <CreateTime>dateTime</CreateTime>
    // </FInfo>
    // <BinaryData>base64Binary</BinaryData>
    // <Annotation>base64Binary</Annotation>
    public long DocId { get; set; }
    public string FileName { get; set; }
    public string MimeType { get; set; }
    public byte[] FileContent { get; set; }
}

public record DmsDocumentAndFileModel(DMSDocumentModel DocumentModel, DMSDocumentFileModel DocumentFile);