namespace amorphie.contract.core.Model.Dys;

public class DMSDocumentModel
{
    public string CustomerNo { get; set; } = default!;
    public string ApplicationNo { get; set; } = default!;
    public string WfInstanceID { get; set; } = default!;
    public string TagId { get; set; } = default!;
    public bool IsExpired { get; set; }
    public long DocId { get; set; }
    public string Title { get; set; } = default!;
    public string Notes { get; set; } = default!;
    public DateTime DocCreatedAt { get; set; }
    public string OwnerId { get; set; } = default!;
    public string Channel { get; set; } = default!;
    public bool IsDeleted { get; set; }

}
