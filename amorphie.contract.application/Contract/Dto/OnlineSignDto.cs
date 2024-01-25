using System.Text.Json.Serialization;

namespace amorphie.contract.application.Contract.Dto
{
    public class OnlineSignDto
    {
        [JsonPropertyName("sca-required")]
        public bool ScaRequired { get; set; }
        [JsonPropertyName("allowed-clients")]
        public List<string> AllovedClients { get; set; }
        [JsonPropertyName("document-model-template")]
        public List<DocumentTemplateDto> DocumentModelTemplate { get; set; }
    }
}
