namespace amorphie.contract.application.Contract.Dto
{
    public class GetContractInstanceResponseDto
    {
        public List<DocumentInstanceDto> DocumentList { get; set; } = new List<DocumentInstanceDto>();
        public List<DocumentInstanceResultDto> DocumentApprovedList { get; set; } = new List<DocumentInstanceResultDto>();
        
        public List<DocumentGroupInstanceDto> DocumentGroupList { get; set; } = new List<DocumentGroupInstanceDto>();

        public string ContractTitle { get; set; }
    }
}
