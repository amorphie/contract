using System;
using amorphie.contract.core.Enum;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Model;
using amorphie.contract.application.Common;

namespace amorphie.contract.application.Contract.Dto.Input
{
    public class ContractDefinitionInputDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public EBankEntity RegistrationType { get; set; }
        public List<Guid> Tags { get; set; }
        public Dictionary<string, string> Titles
        {
            get
            {
                return TitleInput.ToDictionary(x => x.Key, x => x.Value);
            }
        }
        [Required]
        public List<TitleInputDto> TitleInput { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public List<Metadata> Metadatas { get; set; }
        public List<ContractDocumentInputDto> Documents { get; set; }
        public List<ContractDocumentGroupInputDto> DocumentGroups { get; set; }
        public List<ContractValidationInputDto> Validations { get; set; }
    }
}

