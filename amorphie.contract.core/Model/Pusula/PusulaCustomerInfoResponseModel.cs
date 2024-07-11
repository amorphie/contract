namespace amorphie.contract.core.Model.Pusula;

public class PusulaCustomerInfoResponseModel
{
    public int MainBranchCode { get; set; }

    public string CitizenshipNumber { get; set; } = String.Empty;

    public string TaxNo { get; set; } = String.Empty;

    public string GetReference()
    {
        if (!String.IsNullOrWhiteSpace(CitizenshipNumber))
            return CitizenshipNumber;
        else if (!String.IsNullOrWhiteSpace(TaxNo))
            return TaxNo;
        else
            throw new ArgumentNullException("Both CitizenshipNumber and TaxNo are null or empty.");

    }
}