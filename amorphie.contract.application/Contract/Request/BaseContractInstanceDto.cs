using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract.Request
{
    public abstract class BaseContractInstanceDto : BaseHeader
    {
        [Required]
        public required string ContractCode { get; set; }
        [Required]
        public required Guid ContractInstanceId { get; set; }
    }
}