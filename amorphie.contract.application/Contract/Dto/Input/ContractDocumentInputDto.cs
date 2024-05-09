using System;
using amorphie.contract.core.Enum;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract.application.Contract.Dto.Input
{
	public class ContractDocumentInputDto
	{
        [Required]
        public string MinVersion { get; set; }
        [Required]
        public string Code { get; set; }
        public bool Required { get; set; }
        public EUseExisting UseExisting { get; set; }
        public short Order { get; set; }
    }
}

