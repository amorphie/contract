using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.Colleteral;
using amorphie.contract.core.Services.Kafka;
using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace amorphie.contract.infrastructure.Services.Kafka;


public class TSIZLProducer : ITSIZLProducer
{
    private readonly DaprClient _daprClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public TSIZLProducer(DaprClient daprClient, IConfiguration configuration, ILogger logger)
    {
        _daprClient = daprClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task PublishTSIZLData(DoAutomaticEngagementPlainRequestDto requestModel)
    {
        var topicName = KafkaConsts.SendEngagementDataToTSIZLTopicName;

        _logger.Information(
            "Publishing event {@Event} to {PubsubName}.{TopicName}",
            nameof(TSIZLProducer),
            KafkaConsts.KafkaName,
            topicName);

        await _daprClient.PublishEventAsync(KafkaConsts.KafkaName, topicName, requestModel);
    }

}