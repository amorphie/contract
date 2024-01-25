using System.Text.Json.Serialization;

namespace amorphie.contract.application
{
    public class ContractDocumentGroupDetailDto
    {
        public bool Required { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("at-least-required-document")]
        public int AtLeastRequiredDocument { get; set; }
        public DocumentGroupDto ContractDocumentGroup { get; set; }
    }
}
