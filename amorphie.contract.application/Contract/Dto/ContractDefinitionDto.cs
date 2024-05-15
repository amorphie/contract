namespace amorphie.contract.application.Contract.Dto
{
    public class ContractDefinitionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public List<ContractDocumentDetailDto> ContractDocumentDetails { get; set; }

        public List<ContractDocumentGroupDetailDto> ContractDocumentGroupDetails { get; set; }
    }
}
