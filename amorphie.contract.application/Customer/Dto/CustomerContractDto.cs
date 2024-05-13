using System.Text.Json.Serialization;
using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Customer.Dto
{
    public class CustomerContractDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public bool? IsDeleted { get; set; }

        public IEnumerable<CustomerContractDocumentDto> CustomerContractDocuments { get; set; }
        public IEnumerable<CustomerContractDocumentGroupDto> CustomerContractDocumentGroups { get; set; }

        [JsonIgnore]
        public Dictionary<string, string> Titles { get; set; } = default!;

    }

}

