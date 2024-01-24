using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace amorphie.contract.application
{
    public class ContractDocumentGroupDetailDto
    {
        public bool Required { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("at-least-required-document")]
        public int AtLeastRequiredDocument { get; set; }

        //TODO: Umut - Mapping
        public List<DocumentDefinitionDto> DocumentDefinitions { get; set; }
    }
}
