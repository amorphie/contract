namespace amorphie.contract.application.Contract.Dto
{
    public class ContractInstanceDto
    {
        public string ContractCode { get; set; }
        public string Status { get; set; }
        public Guid ContractInstanceId { get; set; }
        public List<DocumentInstanceDto> DocumentList { get; set; } = new List<DocumentInstanceDto>();

        public List<DocumentGroupInstanceDto> DocumentGroupList { get; set; }
    }
}
