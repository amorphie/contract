namespace amorphie.contract.application;

public class KafkaBase
{
    public string Magic { get; set; } = default!;
    public string Type { get; set; } = default!;
    public object? Headers { get; set; }
    public object? MessageSchemaId { get; set; }
    public object? MessageSchema { get; set; }
}

