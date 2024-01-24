using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract.Dto
{
    public class DocumentGroupDto
    {
        public bool Required { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("at-least-required-document")]
        public int AtLeastRequiredDocument { get; set; }
        public List<DocumentDto> Document { get; set; }
    }
}
