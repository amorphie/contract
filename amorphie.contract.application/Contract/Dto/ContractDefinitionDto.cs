namespace amorphie.contract.application.Contract.Dto
{
    public class ContractDefinitionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public List<ContractDocumentDetailDto> ContractDocumentDetails { get; set; }

        
        //TODO: Umut - mapping
        // public List<DocumentGroupDto> DocumentGroups { get; set; }
    }
}
