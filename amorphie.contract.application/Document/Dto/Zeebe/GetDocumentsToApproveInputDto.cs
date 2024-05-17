namespace amorphie.contract.application.Documents.Dto.Zeebe;

public record GetDocumentsToApproveInputDtoZeebe(List<DocumentCodesDto> DocumentList, bool IsRenderOnlineMainFlow);

public record DocumentCodesDto(string Code);
