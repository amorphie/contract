namespace amorphie.contract.application.Migration;

public class KafkaData<T> : KafkaBase
{
    public KafkaMessage<T> Message { get; set; } = new KafkaMessage<T>();
    public T? Data { get; set; }

}