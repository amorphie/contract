using System;
using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Contract.Dto.Input
{
	public class ContractValidationInputDto
	{
        public string DecisionTable { get; set; }
        public EValidationType Type { get; set; }
    }
}

