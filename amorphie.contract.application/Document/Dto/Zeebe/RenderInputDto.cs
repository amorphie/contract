using amorphie.contract.application.Contract.Dto;

namespace amorphie.contract.application.Documents.Dto.Zeebe;

public record RenderInputDto(string ContractCode, Guid ContractInstanceId, List<DocumentInstanceDto> DocumentList, bool IsRenderOnlineMainFlow);


// if IsRenderOnlineMainFlow = false then 
//  {
//   "ContractCode": RenderOnlineSignInputDto.ContractCode,
//   "ContractInstanceId" : RenderOnlineSignInputDto.ContractInstanceId,
//   "DocumentList" :  RenderOnlineSignInputDto.DocumentList,
//   "IsRenderOnlineMainFlow" : IsRenderOnlineMainFlow
//  } 
// else 
//   {
//     "ContractCode": TRXrenderonlinesignstart.Data.entityData.ContractCode,
//     "ContractInstanceId" : TRXrenderonlinesignstart.Data.entityData.ContractInstanceId,
//     "DocumentList": TRXrenderonlinesignstart.Data.entityData.DocumentList,
//     "IsRenderOnlineMainFlow" : IsRenderOnlineMainFlow
//   }


// public string ContractName { get; set; }
// public string? Reference { get; private set; }
// public string BankEntity { get; private set; }