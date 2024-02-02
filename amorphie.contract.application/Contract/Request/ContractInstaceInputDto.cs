namespace amorphie.contract.application.Contract.Request
{
    public class ContractInstanceInputDto
    {
        public string ContractName { get; set; }
        public string Reference { get; set; }
        public LangDto Lang { get; set; } = new LangDto();

        //public string Owner { get; set; }
        //public string CallbackName { get; set; }
        //public Guid ProcessId { get; set; }
        //public ContractProcess Process { get; set; } = default!;
    }
}
