namespace amorphie.contract.application.Contract.Dto
{
    public class ContractInstanceDto
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public List<DocumentInstanceDto> DocumentList { get; set; } = new List<DocumentInstanceDto>();

        public List<DocumentGroupInstanceDto> DocumentGroupList { get; set; }
    }
}
