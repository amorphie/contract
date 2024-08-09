namespace amorphie.contract.core.Model.Documents.Events;

public class DocumentCreationFailedEvent
{
    public Guid DocumentInstanceId { get; set; }
    public required string UserReference { get; set; }
    public long CustomerNo { get; set; }
    public string? Channel { get; set; }
    public string ErrorMessage { get; set; } = default!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

}