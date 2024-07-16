namespace amorphie.contract.application.Migration;

public class KafkaData<T> : KafkaBase
{
    public KafkaMessage<T> Message { get; set; } = default!;
}

