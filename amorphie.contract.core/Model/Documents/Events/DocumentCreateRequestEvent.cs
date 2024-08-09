namespace amorphie.contract.core.Model.Documents.Events;


public class DocumentCreateRequestEvent : DocumentCreationEventBase
{
    public required byte[] FileContent { get; set; } = default!;

    public required string FileName { get; set; } = default!;

    public required string FileContentType { get; set; } = default!;
}
