namespace amorphie.contract.application.Contract.Dto.Zeebe;

public record ValidatedInputDto(List<Guid> DocumentInstanceIds, string? ContractCode, Guid? ContractInstanceId);