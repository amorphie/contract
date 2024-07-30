namespace amorphie.contract.application;

public class KafkaData<T> : KafkaBase
{
    // Qlik Replicate tarafından topic beslendiğinde kullanılır.
    public KafkaMessage<T> Message { get; set; } = new KafkaMessage<T>();

    // dapr ile kafka topic' i beslendiğinde Data dolu gelecektir.
    public T? Data { get; set; }

}