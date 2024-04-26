namespace amorphie.contract.application.Contract.Dto.Zeebe;

public record ContractInputDto(string ContractCode, string ContractInstanceId, bool IsContractMainFlow);
