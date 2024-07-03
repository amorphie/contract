using System.ComponentModel.DataAnnotations;

namespace amorphie.contract.application.Contract.Request
{
    public class GetContractDecisionTableInputDto : BaseHeader
    {
        [Required]
        public required string ContractCode { get; init; }
    }

}
