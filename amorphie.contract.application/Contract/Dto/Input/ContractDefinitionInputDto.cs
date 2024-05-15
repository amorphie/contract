using System;
using amorphie.contract.core.Enum;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Model;

namespace amorphie.contract.application.Contract.Dto.Input
{
    public class ContractDefinitionInputDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public EBankEntity RegistrationType { get; set; }
        public List<Guid> Tags { get; set; }
        [Required]
        public Dictionary<string, string> Titles { get; set; }
        public List<Metadata> Metadatas { get; set; }
        public List<ContractDocumentInputDto> Documents { get; set; }
        public List<ContractDocumentGroupInputDto> DocumentGroups { get; set; }
        public List<ContractValidationInputDto> Validations { get; set; }
    }
}

