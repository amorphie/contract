using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract.Dto.Input
{
    public class CancelContractInputDto
    {
        [Required]
        public required string ContractCode { get; set; }
        public Guid? ContractInstanceId { get; set; }
    }
}