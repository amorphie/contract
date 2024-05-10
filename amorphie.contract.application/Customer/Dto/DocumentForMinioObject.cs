using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Customer
{

    public class DocumentForMinioDto
    {
        public Guid Id { get; set; }
        public Guid DocumentDefinitionId { get; set; }
        public ApprovalStatus Status { get; set; }
        public string DocumentContentId { get; set; }
    }
}

