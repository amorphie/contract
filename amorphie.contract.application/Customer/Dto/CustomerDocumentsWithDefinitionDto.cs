using System.Text.Json.Serialization;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Customer.Dto
{
    public class CustomerDocumentsWithDefinitionDto
    {
        public CustomerDocumentDto? CustomerDocumentDto { get; set; }
        public CustomerDocumentDefinitionDto? CustomerDocumentDefinitionDto { get; set; }
    }

}

