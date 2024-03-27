using System.Text.Json.Serialization;
using amorphie.core.Base;

namespace amorphie.contract.application
{

    public class DocumentGroupDto : BaseDto
    {
        public List<DocumentDefinitionDto> DocumentDefinitions { get; set; } = default!;

        public string Status { get; set; } = default!;

        [JsonIgnore]
        public Dictionary<string, string> Titles { get; set; } = default!;

    }
}