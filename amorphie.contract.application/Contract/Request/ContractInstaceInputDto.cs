using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract.application.Contract.Request
{
    public class ContractInstanceInputDto : BaseHeader
    {

        [Required]
        public required string ContractCode { get; set; }
        [Required]
        public required Guid ContractInstanceId { get; set; }

    }

}
