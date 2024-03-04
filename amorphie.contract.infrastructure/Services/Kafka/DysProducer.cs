using amorphie.contract.core.Enum;
using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace amorphie.contract.infrastructure.Services.Kafka;


public class DysProducer
{
    private readonly DaprClient _daprClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public DysProducer(DaprClient daprClient, IConfiguration configuration, ILogger logger)
    {
        _daprClient = daprClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task PublishDysData(CancellationToken cancellationToken)
    {
        var topicName = KafkaConsts.SendDocumentInstanceDataToDYSTopicName;

        _logger.Information(
            "Publishing event {@Event} to {PubsubName}.{TopicName}",
            nameof(DysProducer),
            KafkaConsts.KafkaName,
            topicName);

        // We need to make sure that we pass the concrete type to PublishEventAsync,
        // which can be accomplished by casting the event to dynamic. This ensures
        // that all event fields are properly serialized.
        await _daprClient.PublishEventAsync(KafkaConsts.KafkaName, topicName, "123", cancellationToken);
    }



}