using amorphie.contract.core.Entity.Base;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Model.History;
public class DocumentGroupHistoryModel : BaseEntity
{
    public ApprovalStatus Status { get; set; } = default!;
    public ICollection<DocumentGroupDetail> DocumentGroupDetails { get; set; } = new List<DocumentGroupDetail>();
    public ICollection<DocumentGroupHistory>? DocumentGroupHistories { get; set; } = new List<DocumentGroupHistory>();

}