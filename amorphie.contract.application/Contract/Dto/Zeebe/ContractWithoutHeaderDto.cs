namespace amorphie.contract.application.Contract.Dto.Zeebe;

public class ContractWithoutHeaderDto
{
    public string? Reference { get; set; }
    public string BankEntity { get; set; }
    public string? CustomerNo { get; set; }

    public bool CheckForNull()
    {
        return !String.IsNullOrWhiteSpace(Reference) && !String.IsNullOrWhiteSpace(BankEntity) && !String.IsNullOrWhiteSpace(CustomerNo);
    }
}

