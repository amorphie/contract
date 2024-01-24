using amorphie.contract.core.Model.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
