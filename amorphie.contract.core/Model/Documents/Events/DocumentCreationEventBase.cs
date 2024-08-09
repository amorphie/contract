namespace amorphie.contract.core.Model.Documents.Events;

public class DocumentCreationEventBase
{
    public Guid? ContractInstanceId { get; set; }
    public string? ContractCode { get; set; }

    public required Guid DocumentInstanceId { get; set; }
    public required string DocumentCode { get; set; }
    public required string DocumentVersion { get; set; }
    public required string UserReference { get; set; }
    public long CustomerNo { get; set; }
    public required string Channel { get; set; }

}
