using amorphie.contract.core.Model.Dys;

namespace amorphie.contract.core.Services.Kafka;
public interface IDysProducer
{
    Task PublishDysData(DocumentDysRequestModel requestModel);
    Task PublishDysDataAgainToContract(object kafkaDataModelDto);
}
