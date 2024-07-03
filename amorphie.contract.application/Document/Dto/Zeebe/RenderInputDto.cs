using amorphie.contract.application.Contract.Dto;

namespace amorphie.contract.application.Documents.Dto.Zeebe;

public record RenderInputDto(string ContractCode, Guid ContractInstanceId, List<DocumentInstanceDto> DocumentList, bool IsRenderOnlineMainFlow);



