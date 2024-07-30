namespace amorphie.contract.application;

public class KafkaMessage<T>
{
    public T Data { get; set; } = default!;
    public object? BeforeData { get; set; }
    public object? Headers { get; set; }
}

