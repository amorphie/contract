using amorphie.contract.application.Contract.Dto.Zeebe;
using amorphie.contract.core.Model.Proxy;

namespace amorphie.contract.application
{
    public record RenderApprovalDocument(List<DocumentForApproval> DocumentsForApproval, ContractWithoutHeaderDto? ContractWithoutHeader);

}