namespace amorphie.contract.core.Model.Documents.Events;

public class DocumentCreatedEvent : DocumentCreationEventBase
{
    /// <summary>
    /// For downloading document
    /// </summary>
    public Guid DocumentContentId { get; set; }
}
