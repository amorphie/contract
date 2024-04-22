namespace amorphie.contract.application.Contract.Dto.Zeebe;

public class ContractInstanceZeebeDto
{
    public string ContractName { get; set; }
    public string? Reference { get; private set; }
    public string BankEntity { get; private set; }
}