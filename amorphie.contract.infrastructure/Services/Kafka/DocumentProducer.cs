using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.Documents.Events;
using amorphie.contract.core.Services.Kafka;
using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace amorphie.contract.infrastructure.Services.Kafka;


public class DocumentProducer : IDocumentProducer
{
    private readonly DaprClient _daprClient;
    private readonly ILogger _logger;

    public DocumentProducer(DaprClient daprClient, ILogger<DocumentProducer> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    public async Task PublishDocumentCreatedEvent(DocumentCreatedEvent documentCreatedEvent)
    {

        ArgumentNullException.ThrowIfNull(documentCreatedEvent, nameof(documentCreatedEvent));
        ArgumentNullException.ThrowIfNull(documentCreatedEvent.DocumentCode, nameof(documentCreatedEvent.DocumentCode));
        ArgumentNullException.ThrowIfNull(documentCreatedEvent.UserReference, nameof(documentCreatedEvent.UserReference));
        ArgumentNullException.ThrowIfNull(documentCreatedEvent.DocumentVersion, nameof(documentCreatedEvent.DocumentVersion));

        await _daprClient.PublishEventAsync(KafkaConsts.KafkaName, KafkaConsts.DocumentCreatedEventTopicName, documentCreatedEvent);

        _logger.LogInformation(
              "published document created event {@Event} to {PubsubName}.{TopicName} -- {DocumentInstanceId}",
              nameof(DocumentCreatedEvent),
              KafkaConsts.KafkaName,
              KafkaConsts.DocumentCreatedEventTopicName,
              documentCreatedEvent.DocumentInstanceId);
    }

    public async Task PublishDocumentCreationFailedEvent(DocumentCreationFailedEvent documentCreationFailedEvent)
    {

        ArgumentNullException.ThrowIfNull(documentCreationFailedEvent, nameof(documentCreationFailedEvent));
        ArgumentNullException.ThrowIfNull(documentCreationFailedEvent.UserReference, nameof(documentCreationFailedEvent.UserReference));
        ArgumentNullException.ThrowIfNull(documentCreationFailedEvent.ErrorMessage, nameof(documentCreationFailedEvent.ErrorMessage));

        await _daprClient.PublishEventAsync(KafkaConsts.KafkaName, KafkaConsts.DocumentCreationFailedEventTopicName, documentCreationFailedEvent);

        _logger.LogInformation(
              "published document creation failed event {@Event} to {PubsubName}.{TopicName}",
              nameof(DocumentCreatedEvent),
              KafkaConsts.KafkaName,
              KafkaConsts.DocumentCreationFailedEventTopicName);
    }
}