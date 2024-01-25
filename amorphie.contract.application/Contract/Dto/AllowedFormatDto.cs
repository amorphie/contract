using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract.Dto
{
    public class AllowedFormatDto
    {
        public string Format { get; set; }
        [JsonPropertyName("max-size-kilobytes")]
        public int MaxSizeKilobytes { get; set; }
    }
}
