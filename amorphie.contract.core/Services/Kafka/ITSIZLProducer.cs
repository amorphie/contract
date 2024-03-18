using amorphie.contract.core.Model.Colleteral;

namespace amorphie.contract.core.Services.Kafka;
public interface ITSIZLProducer
{
    Task PublishTSIZLData(DoAutomaticEngagementPlainRequestDto requestModel);
}
