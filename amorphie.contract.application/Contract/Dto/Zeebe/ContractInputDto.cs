namespace amorphie.contract.application.Contract.Dto.Zeebe;


public class ContractInputDto
{
    public string ContractCode { get; set; }
    public string ContractInstanceId { get; set; }

    public bool IsContractMainFlow { get; set; }

}
