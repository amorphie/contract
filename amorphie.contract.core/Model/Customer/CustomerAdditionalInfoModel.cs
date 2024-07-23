using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Model.Customer;


public class CustomerAdditionalInfoModel
{
    public string TaxNo { get; set; } = string.Empty;
    public string CitizenshipNumber { get; set; } = string.Empty;
}