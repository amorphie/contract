using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Contract.Request
{
    public class ContractGetExistInputDto
    {
        public string Code { get; set; }
        public EBankEntity EBankEntity { get; set; }
    }
}
