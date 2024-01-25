namespace amorphie.contract.application.Contract.Dto
{

    public class ContractDocumentDetailDto
    {
        public string? UseExisting { get; set; }
        public string? MinVersion { get; set; }
        public bool Required { get; set; }
        public DocumentDefinitionDto DocumentDefinition { get; set; }

    }
}
