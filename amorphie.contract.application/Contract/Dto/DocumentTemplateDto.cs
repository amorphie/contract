using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract.Dto
{
    public class DocumentTemplateDto
    {
        public string Name { get; set; }
        [JsonPropertyName("min-version")]
        public string MinVersion { get; set; }
    }
}
