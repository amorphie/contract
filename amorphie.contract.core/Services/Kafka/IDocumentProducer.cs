using amorphie.contract.core.Model.Documents.Events;

namespace amorphie.contract.core.Services.Kafka;

public interface IDocumentProducer
{
    Task PublishDocumentCreatedEvent(DocumentCreatedEvent documentCreatedEvent);
    Task PublishDocumentCreationFailedEvent(DocumentCreationFailedEvent documentCreationFailedEvent);
}
