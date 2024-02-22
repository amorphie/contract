namespace amorphie.contract.application.Contract.Dto
{
    public class ContractInstanceDto
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public List<DocumentInstanceDto> Document { get; set; } = new List<DocumentInstanceDto>();

        // public List<ContractDocumentGroupDetailDto> DocumentGroupDetails { get; set; }
    }
}
