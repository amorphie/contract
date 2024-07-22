using amorphie.contract.core.Enum;
using amorphie.contract.core.Model.Dys;
using amorphie.contract.core.Services.Kafka;
using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace amorphie.contract.infrastructure.Services.Kafka;


public class DysProducer : IDysProducer
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

    public async Task PublishDysData(DocumentDysRequestModel requestModel)
    {
        var topicName = KafkaConsts.SendDocumentInstanceDataToDYSTopicName;

        _logger.Information(
            "Publishing event {@Event} to {PubsubName}.{TopicName}",
            nameof(DysProducer),
            KafkaConsts.KafkaName,
            topicName);

        await _daprClient.PublishEventAsync(KafkaConsts.KafkaName, topicName, requestModel);
    }

    /// <summary>
    /// Contract uygulamasının dokümanları tekrar migrate etmesi için ya da belirli bir TagID dokümanlarının tekrar migrate edilmesi için bu topic' e data yazılır.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    public async Task PublishDysDataAgainToContract(object kafkaDataModelDto)
    {
        var topicName = KafkaConsts.DysDocumentTag;

        _logger.Information(
            "Publishing event {@Event} to {PubsubName}.{TopicName}",
            nameof(DysProducer),
            KafkaConsts.KafkaName,
            topicName);

        await _daprClient.PublishEventAsync(KafkaConsts.KafkaName, topicName, kafkaDataModelDto);
    }
}