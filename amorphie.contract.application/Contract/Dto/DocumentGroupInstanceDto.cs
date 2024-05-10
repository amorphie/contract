using amorphie.contract.core.Enum;

namespace amorphie.contract.application
{
    public class DocumentGroupInstanceDto
    {
        public DocumentGroupInstanceDto()
        {
            Status = ApprovalStatus.InProgress.ToString();
        }
        public bool Required { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }

        public int AtLeastRequiredDocument { get; set; }
        public DocumentGroupDetailInstanceDto DocumentGroupDetailInstance { get; set; }
    }
}
