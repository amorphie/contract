namespace amorphie.contract.core.Model.Colleteral;

public class DoAutomaticEngagementPlainRequestDto
{
    public int AccountBranchCode { get; set; }
    public int AccountNumber { get; init; }
    public int AccountSuffix { get; init; }
    public string CurrencyCode { get; init; }
    public DateTime EngagementDate { get; init; }
    public string EngagementType { get; init; }
    public string EngagementKind { get; init; }
    public decimal EngagementAmount { get; init; }
    public string UserCode { get; init; }
    public DoAutomaticEngagementPlainRequestDto(int accountNumber, string engagementKind)
    {
        ArgumentException.ThrowIfNullOrEmpty(engagementKind, nameof(engagementKind));

        if (accountNumber <= 0)
            throw new ArgumentException("accountNumber must be greater than zero");

        AccountNumber = accountNumber;
        AccountSuffix = 0;
        CurrencyCode = "TRY";
        EngagementKind = engagementKind;
        EngagementDate = DateTime.Now;
        EngagementType = "G";
        EngagementAmount = 0.01m;
        UserCode = "EBT\\CONTRACT";
    }
    public DoAutomaticEngagementPlainRequestDto()
    {

    }

    public void SetAccountBranchCode(int accountBranchCode)
    {
        AccountBranchCode = accountBranchCode;
    }

    public override string ToString()
    {
        return $"{EngagementKind} {AccountNumber} {AccountBranchCode}";
    }
}



