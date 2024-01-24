using amorphie.core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract.Dto
{
    public class DocumentDto
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public List<MultilanguageText> MultilanguageText { get; set; }
        public string Status { get; set; }
        public bool Required { get; set; }
        public UploadDto Upload { get; set; }
        public bool Render { get; set; }
        [JsonPropertyName("online-sign")]
        public OnlineSignDto OnlineSign { get; set; }
        public string Version { get; set; }
    }
}
