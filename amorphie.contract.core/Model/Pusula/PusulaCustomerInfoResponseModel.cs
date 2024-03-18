namespace amorphie.contract.core.Model.Pusula;

public class PusulaCustomerInfoResponseModel
{
    public int MainBranchCode { get; private set; }

    public PusulaCustomerInfoResponseModel(int mainBranchCode)
    {
        MainBranchCode = mainBranchCode;
    }

}