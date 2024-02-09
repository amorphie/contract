using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Contract.Request
{
    public class ContractInstanceInputDto
    {
        public string ContractName { get; set; }
        public string Reference { get; set; }
        public string LangCode { get; set; }
        public EBankEntity EBankEntity { get; set; }
        //public string Owner { get; set; }
        //public string CallbackName { get; set; }
        //public Guid ProcessId { get; set; }
        //public ContractProcess Process { get; set; } = default!;
    }
    public class ContractInstanceSoftInputDto
    {
        public string ContractName { get; set; }
        public string Reference { get; set; }

    }
}
