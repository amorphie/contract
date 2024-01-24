using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract.Dto
{
    public class UploadDto
    {
        [JsonPropertyName("sca-required")]
        public bool ScaRequired { get; set; }
        [JsonPropertyName("allowed-clients")]
        public List<string> AllowedClients { get; set; }
        [JsonPropertyName("allowed-formats")]
        public List<AllowedFormatDto> AllowedFormats { get; set; }
    }
}
