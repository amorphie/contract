namespace amorphie.contract.application;

public class KafkaErrorMessageDto
{
    public string ErrorMessage { get; set; } = default!;
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}