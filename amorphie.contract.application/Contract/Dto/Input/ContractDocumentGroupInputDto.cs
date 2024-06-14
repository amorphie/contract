using System;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract.application.Contract.Dto.Input
{
	public class ContractDocumentGroupInputDto
	{
        public ushort AtLeastRequiredDocument { get; set; }
        [Required]
        public Guid Id { get; set; }
        public bool Required { get; set; }
    }
}

