using amorphie.contract.core.Model;

namespace amorphie.contract.application.Contract.Dto
{
    public record ContractDecisionDto(string DecisionTableId, List<MetadataDto> Metadata);
}