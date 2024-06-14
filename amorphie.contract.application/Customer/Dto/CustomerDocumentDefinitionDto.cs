using System.Text.Json.Serialization;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Customer.Dto
{
    public class CustomerDocumentDefinitionDto
    {
        public Guid? DocumentId { get; set; }
        public Guid DocumentDefinitionId { get; set; }
        public string? Code { get; set; }
        public string? Version { get; set; }
        public OnlineSignDto? OnlineSign { get; set; }
        public Dictionary<string, string>? Titles { get; set; } = default!;
    }

}

